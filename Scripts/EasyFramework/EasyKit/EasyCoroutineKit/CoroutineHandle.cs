using System;
using System.Runtime.CompilerServices;

namespace EasyFramework
{
    /// <summary>
    /// 包装协程，提供协程的取消功能，并且支持使用await与yield return语法
    /// CoroutineHandle具有时效性，当协程完成或取消时，会自动回收;
    /// <para>注意:因为CoroutineHandle是可回收的，所以不建议长期保留CoroutineHandle的引用</para>
    /// </summary>
    public class CoroutineHandle : IYieldAble,IAwaitAble,ICancelAble,ICompleteAble,IDoneAble,IRecycleable
    {
        protected CoroutineHandle() { }
        private static readonly EasyPool<CoroutineHandle> Pool = new EasyPool<CoroutineHandle>(() => new CoroutineHandle(), null, null, 64);
        public static CoroutineHandle Fetch()
        {
            var handle= Pool.Fetch();
            ((IRecycleable)handle).Recycle();
            handle.IsCanceled = false;
            handle.IsCompleted = false;
            return handle;
        }
        public event Action Completed;
        public event Action Cancelled;
        public event Action Done;
        public bool IsDone => IsCanceled || IsCompleted;
        public bool IsCanceled { get; protected set; }
        public bool IsCompleted { get; protected set; }

        public bool MoveNext()=>!IsDone;
        public void Reset() {}
        public object Current => null;

        public CoroutineHandle GetAwaiter()=> this;
        public void GetResult(){}
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (IsDone)
            {
                continuation?.Invoke();
            }
            else
            {
                Cancelled += continuation;
                Completed += continuation;
            }
        }
        void ICancelAble.Cancel()
        {
            if(IsDone)
                return;
                
            IsCanceled = true;
            OnCancel();
        }

        internal void Complete()
        {
            if (IsDone)
                return;

            IsCompleted = true;
            OnCompleted();
        }

        private void OnCancel()
        {
            EasyCoroutine.StopCoroutine(this);
            Cancelled?.Invoke();
            Done?.Invoke();
            EasyCoroutine.RecycleCoroutineHandle(this);
        }
        private void OnCompleted()
        {
            Completed?.Invoke();
            Done?.Invoke();
            EasyCoroutine.RecycleCoroutineHandle(this);
        }

        public bool IsRecycled { get; set; }

        void IRecycleable.Recycle()
        {
            Cancelled = null;
            Completed = null;
        }
        internal virtual void Recycle() => Pool.Recycle(this);
    }
    /// <summary>
    /// 包装协程，提供协程的取消功能，并且支持使用await与yield return语法
    /// CoroutineHandle具有时效性，当协程完成或取消时，会自动回收;
    /// <para>注意:因为CoroutineHandle是可回收的，所以不建议长期保留CoroutineHandle的引用</para>
    /// </summary>
    public class CoroutineHandle<T> : CoroutineHandle,IAwaitAble<T>
    {
        protected CoroutineHandle()
        { 
            Pool.SetOnFetch(ResetHandle);
        }
        private static readonly EasyPool<CoroutineHandle<T>> Pool = new(() => new CoroutineHandle<T>(), null, null, 64);
        public new static CoroutineHandle<T> Fetch()
        {
            var handle= Pool.Fetch();
            ((IRecycleable)handle).Recycle();
            handle.IsCanceled = false;
            handle.IsCompleted = false;
            return handle;
        }
        public static CoroutineHandle<T> Fetch(T result)
        {
            var handle= Pool.Fetch();
            ((IRecycleable)handle).Recycle();
            handle.IsCanceled = false;
            handle.IsCompleted = false;
            handle.SetResult(result);
            return handle;
        }

        public T Result { get; private set; }
        public new CoroutineHandle<T> GetAwaiter()=> this;
        public new T GetResult()=> Result;

        private void ResetHandle(CoroutineHandle handle)
        {
            Result = default;
        }
        public void SetResult(T result)
        {
            Result = result;
            Complete();
        }
        internal override void Recycle() => Pool.Recycle(this);
    }
}