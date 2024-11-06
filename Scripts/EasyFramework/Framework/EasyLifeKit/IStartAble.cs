

namespace EasyFramework
{
    public interface IStartAble: IInitAble
    {
        bool IsStart { get; set; }
        IEasyEvent StartEvent { get;}

        static void EnableStartAble(IStartAble startAble)=>EasyLifeCycle.Update.RegisterEvent(startAble.Start);
        static void DisableStartAble(IStartAble startAble)=>EasyLifeCycle.Update.UnRegisterEvent(startAble.Start);
        void Start()
        {
            if (IsStart) return;
            IsStart = true;
            OnStart();
            StartEvent.BaseInvoke();
            EasyLifeCycle.Update.UnRegisterEvent(Start);
        }
        protected void OnStart();
    }
    // public interface IStartAble<T> : IStartAble
    // {
    //     new EasyEvent<T> StartEvent { get; set; }
    //     IEasyEvent IStartAble.StartEvent=> StartEvent;
    //
    //     void Start(T t)
    //     {
    //         OnStart();
    //         StartEvent.Invoke(t);
    //     }
    // }
    // public interface IStartAble<T1, T2> : IStartAble
    // {
    //     new EasyEvent<T1, T2> StartEvent { get; set; }
    //     IEasyEvent IStartAble.StartEvent => StartEvent;
    //
    //     void Start(T1 t1, T2 t2)
    //     {
    //         OnStart();
    //         StartEvent.Invoke(t1, t2);
    //     }
    // }
}