using System;

namespace EasyFramework
{
    public interface IEasyAction
    {
        long ActionID { get; }
        float RunTime { get; }
        bool IsPause { get; }
        int LoopCount { get;}
        int CurrentLoopCount { get;}


        void Start();
        void Pause();
        void Resume();
        bool Update(float deltaTime);
        void Cancel();
        void Complete();
        bool OnActionUpdate(float deltaTime);
        void OnActionCompleted();
        void OnActionCancel();
        void OnActionEnd();
        
        
        void SetLoopCount(int loopCount);
        void SetCurrentLoopCount(int currentLoopCount);
    }

    public interface IEasyActionEvent: ICompleteAble,ICancelAble
    {
        public event Action Running;
        public event Action End;
    }
}