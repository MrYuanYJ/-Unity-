namespace EasyFramework
{
    public abstract class AEasyExLoopAction: AEasyAction,IEasyExLoopAction
    {
        public LoopType LoopType { get; set; }
        public float MaxProgress { get; protected set; }
        public bool IsReverse { get; protected set; }
  
        public void SetMaxProgress(float maxProgress)=> MaxProgress = maxProgress;

        public void SetReverse(bool isReverse)=> IsReverse = isReverse;

        protected virtual void Reset(int loopCount,LoopType loopType)
        {
            base.Reset(loopCount);
            LoopType = loopType;
            MaxProgress = 1f;
            IsReverse = false;
        }
    }
}