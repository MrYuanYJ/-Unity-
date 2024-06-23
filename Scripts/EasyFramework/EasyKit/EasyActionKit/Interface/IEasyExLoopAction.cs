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
        LoopType LoopType { get; set; }
        float MaxProgress { get; }
        bool IsReverse { get; }

        void SetMaxProgress(float maxProgress);
        void SetReverse(bool isReverse);
    }
}