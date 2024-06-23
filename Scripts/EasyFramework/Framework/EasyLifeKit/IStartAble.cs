using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IStartAble
    {
        bool IsStart { get; set; }
        IEasyEvent StartEvent { get;}
        void Start()
        {
            if (IsStart) return;
            IsStart = true;
            OnStart();
            StartEvent.BaseInvoke();
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