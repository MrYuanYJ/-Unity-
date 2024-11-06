using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFramework
{
    public class TaskHandle: IYieldAble,IAwaitAble,IRecycleable
    {
        protected TaskHandle(){}
        private static readonly EasyPool<TaskHandle> Pool = new(() => new TaskHandle(), null, null, 32);

        public static TaskHandle Fetch(Task task)
        {
            var handle =Pool.Fetch();
            handle.SetTask(task);
            return handle;
        }

        public event Action Completed;
        public event Action Canceled;
        public bool IsDone => IsCanceled || IsCompleted;
        public bool IsCanceled => _task.IsCanceled;
        public bool IsCompleted => _task.IsCompleted;

        public bool MoveNext()
        {
            if (IsDone) return false;
            if (IsCanceled)
                OnCancel();
            else if (IsCompleted)
                OnCompleted();
            return true;
        }
        public void Reset() {}
        public object Current => null;

        private Task _task;
        private CancellationToken _token;
        private void SetTask(Task task)=> _task = task;

        public TaskHandle GetAwaiter()=> this;
        public void GetResult(){}
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (IsDone)
            {
                if (IsCanceled)
                    OnCancel();
                else if (IsCompleted)
                    OnCompleted();
                continuation?.Invoke();
            }
            else
            {
                Canceled += continuation;
                Completed += continuation;
            }
        }

        private void OnCancel()
        {
            Canceled?.Invoke();
            Pool.Recycle(this);
        }
        private void OnCompleted()
        {
            Completed?.Invoke();
            Pool.Recycle(this);
        }

        public bool IsRecycled { get; set; }

        void IRecycleable.Recycle()
        {
            Canceled = null;
            Completed = null;
        }
    }

    public class TaskHandle<T> : TaskHandle, IAwaitAble<T>
    {
        protected TaskHandle(){}
        private static readonly EasyPool<TaskHandle<T>> Pool = new(() => new TaskHandle<T>(), null, null, 32);
        public static TaskHandle<T> Fetch(Task<T> task)
        {
            var handle=Pool.Fetch();
            handle.SetTask(task);
            return handle;
        }

        public T Result => _task.Result;
        public new TaskHandle<T> GetAwaiter()=> this;
        public new T GetResult()=> Result;
        private Task<T> _task;
        private void SetTask(Task<T> task)=> _task = task;
    }

    public static class TaskHandleExtensions
    {
        public static TaskHandle ToHandle(this Task task)
        {
            return TaskHandle.Fetch(task);
        }
        public static TaskHandle<T> ToHandle<T>(this Task<T> task)
        {
            return TaskHandle<T>.Fetch(task);
        }
    }
}