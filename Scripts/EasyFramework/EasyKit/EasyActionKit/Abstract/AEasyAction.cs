using System;

namespace EasyFramework
{
    public abstract class AEasyAction: IEasyAction,IEasyActionEvent
    {
        public long ActionID { get; protected set; }
        public float RunTime { get; protected set; }
        public bool IsPause { get; protected set; }
        public int LoopCount { get; protected set; }
        public int CurrentLoopCount { get; protected set; }
        
        void IEasyAction.Start()=>EasyActionSingleton.AddAction(this);
        void IEasyAction.Pause()=> IsPause = true;
        void IEasyAction.Resume()=> IsPause = false;
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
        void IEasyAction.Cancel()=> EasyActionSingleton.RemoveAction(this,(this as IEasyAction).OnActionCancel);
        bool IEasyAction.OnActionUpdate(float deltaTime)
        {
            OnRunning?.Invoke();
            return OnActionUpdate(deltaTime);
        }

        void IEasyAction.OnActionCompleted()
        {
            OnActionCompleted();
            OnCompleted?.Invoke();
        }

        void IEasyAction.OnActionCancel()
        {
            OnActionCancel();
            OnCanceled?.Invoke();
        }

        void IEasyAction.OnActionEnd()
        {
            OnActionEnd();
            OnEnd?.Invoke();
        }

        protected abstract bool OnActionUpdate(float deltaTime);
        protected abstract void OnActionCompleted();
        protected abstract void OnActionCancel();
        protected abstract void OnActionEnd();

        public event Action OnRunning;
        public event Action OnCompleted;
        public event Action OnCanceled;
        public event Action OnEnd;

        private void ClearEvents()
        {
            OnRunning = null;
            OnCompleted = null;
            OnCanceled = null;
            OnEnd = null;
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
    }
}
