using Unity.VisualScripting;

namespace EasyFramework
{
    public abstract class AEasyProgressAction: AEasyAction,IEasyProgressAction
    {
        protected abstract override bool OnActionUpdate(float deltaTime);

        protected abstract override void OnActionCompleted();

        protected abstract override void OnActionCancel();
        protected abstract override void OnActionEnd();

        public float Progress { get; protected set; }
        public float Duration { get; protected set; }

        protected virtual void Reset(float duration, int loopCount)
        {
            base.Reset(loopCount);
            Duration = duration;
            Progress = 0;
        }
    }

    public abstract class AEasyProgressAndExLoopAction : AEasyProgressAction, IEasyExLoopAction
    {
        public LoopType LoopType { get; protected set; }
        public float MaxProgress { get; protected set; }
        public bool IsReverse { get; protected set; }
        
        public void SetLoopType(LoopType loopType)=> LoopType = loopType;
        public void SetMaxProgress(float maxProgress)=> MaxProgress = maxProgress;
        public void SetReverse(bool isReverse)=> IsReverse = isReverse;

        protected virtual void Reset(float duration,int loopCount,LoopType loopType)
        {
            base.Reset(duration,loopCount);
            LoopType = loopType;
            MaxProgress = 1;
            IsReverse = false;
        }
    }

    public abstract class AEasyProgressAndLerpAction<T> : AEasyProgressAction, IEasyLerpAction<T>
    {
        public IEase Ease { get;  protected set; }
        public T StartValue { get; protected set; }
        public T EndValue { get; protected set; }
        public abstract T GetValue();

        protected virtual void Reset(float duration, int loopCount, T startValue, T endValue)
        {
            base.Reset(duration, loopCount);
            Ease = new Ease(EaseType.Linear);
            StartValue = startValue;
            EndValue = endValue;
        }
    }

    public abstract class AEasyProgressAndLerpAndExLoopAction<T> : AEasyProgressAction, IEasyLerpAction<T>,
        IEasyExLoopAction
    {
        public IEase Ease { get;  protected set; }
        public T StartValue { get;  protected set; }
        public T EndValue { get;  protected set; }

        public LoopType LoopType { get;  protected set; }
        public float MaxProgress { get;  protected set; }
        public bool IsReverse { get;  protected set; }
        public abstract T GetValue();
        public void SetLoopType(LoopType loopType)=> LoopType = loopType;
        public void SetMaxProgress(float maxProgress)=> MaxProgress = maxProgress;
        public void SetReverse(bool isReverse)=> IsReverse = isReverse;

        protected virtual void Reset(float duration, int loopCount, T startValue, T endValue, LoopType loopType)
        {
            base.Reset(duration, loopCount);
            Ease = new Ease(EaseType.Linear);
            StartValue = startValue;
            EndValue = endValue;
            LoopType = loopType;
            MaxProgress = 1;
            IsReverse = false;
        }
    }
}