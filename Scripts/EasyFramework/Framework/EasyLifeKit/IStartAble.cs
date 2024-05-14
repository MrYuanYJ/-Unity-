using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IStartAble
    {
        IEasyEvent StartEvent { get;}
        void Start()
        {
            OnStart();
            StartEvent.Invoke();
        }
        void OnStart();
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