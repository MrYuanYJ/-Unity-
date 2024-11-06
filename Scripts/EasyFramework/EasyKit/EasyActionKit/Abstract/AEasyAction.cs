using System;
using System.Runtime.CompilerServices;

namespace EasyFramework
{
    public abstract class AEasyAction: IEasyAction,IEasyActionEvent,IYieldAble,IAwaitAble
    {
        public long ActionID { get; protected set; }
        public float RunTime { get; protected set; }
        public bool IsPause { get; protected set; }
        public int LoopCount { get; protected set; }
        public int CurrentLoopCount { get; protected set; }
        public bool IsDone =>IsCompleted || IsCanceled;
        public bool IsCanceled { get; protected set; }
        public bool IsCompleted { get; protected set; }
        
        public void Start()=>EasyActionSingleton.AddAction(this);
        public void Pause()=> IsPause = true;
        public void Resume()=> IsPause = false;
        public void Cancel()
        {
            if (IsDone) return;
            IsCanceled = true;
            EasyActionSingleton.RemoveAction(this, (this as IEasyAction).OnActionCancel);
        }

        bool IEasyAction.Update(float deltaTime)
        {
            if (IsPause) return false;
            RunTime += deltaTime;
            return ((IEasyAction)this).OnActionUpdate(deltaTime);
        } 
        void IEasyAction.Complete()
        {
            if (CurrentLoopCount >= 0)
                CurrentLoopCount++;
            if (LoopCount <= 0 || CurrentLoopCount < LoopCount)
            {
                (this as IEasyAction).OnActionCompleted();
            }
            else
            {
                EasyActionSingleton.RemoveAction(this,null);
                (this as IEasyAction).OnActionCompleted();
                (this as IEasyAction).OnActionEnd();
            }
        }
        bool IEasyAction.OnActionUpdate(float deltaTime)
        {
            Running?.Invoke();
            return OnActionUpdate(deltaTime);
        }

        void IEasyAction.OnActionCompleted()
        {
            IsCompleted = true;
            OnActionCompleted();
            Completed?.Invoke();
        }

        void IEasyAction.OnActionCancel()
        {
            OnActionCancel();
            Cancelled?.Invoke();
        }

        void IEasyAction.OnActionEnd()
        {
            OnActionEnd();
            End?.Invoke();
        }

        protected abstract bool OnActionUpdate(float deltaTime);
        protected abstract void OnActionCompleted();
        protected abstract void OnActionCancel();
        protected abstract void OnActionEnd();

        public event Action Running;
        public event Action Completed;
        public event Action Cancelled;
        public event Action End;

        private void ClearEvents()
        {
            Running = null;
            Completed = null;
            Cancelled = null;
            End = null;
        }

        protected virtual void Reset(int loopCount)
        {
            ClearEvents();
            RunTime = 0;
            IsPause = false;
            LoopCount = loopCount;
            CurrentLoopCount = 0;
        }
        void IEasyAction.SetLoopCount(int loopCount) => LoopCount = loopCount;
        void IEasyAction.SetCurrentLoopCount(int currentLoopCount)=> CurrentLoopCount = currentLoopCount;
        public bool MoveNext()=>!IsDone;
        public void Reset() {}
        public object Current => null;
        public AEasyAction GetAwaiter()=> this;
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
    }
}
