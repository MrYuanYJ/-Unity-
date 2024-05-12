namespace EasyFramework
{
    public enum LoopType
    {
        ReStart,
        YoYo,
        Incremental,
    }
    public interface IEasyExLoopAction
    {
        LoopType LoopType { get;}
        float MaxProgress { get;}
        bool IsReverse { get;}

        void SetLoopType(LoopType loopType);
        void SetMaxProgress(float maxProgress);
        void SetReverse(bool isReverse);
    }
}