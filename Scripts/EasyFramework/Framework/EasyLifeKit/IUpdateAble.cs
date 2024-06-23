using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IUpdateAble : IEasyLife
    {
        IEasyEvent UpdateEvent { get; }

        void Update()
        {
            OnUpdate();
            UpdateEvent.BaseInvoke();
        }
        protected void OnUpdate();
    }

    // public interface IUpdateAble<T> : IUpdateAble
    // {
    //     new EasyEvent<T> UpdateEvent { get; set; }
    //     IEasyEvent IUpdateAble.UpdateEvent => UpdateEvent;
    //     
    //     void Update(T t)
    //     {
    //         OnUpdate();
    //         UpdateEvent.Invoke(t);
    //     }
    // }
    // public interface IUpdateAble<T1, T2> : IUpdateAble
    // {
    //     new EasyEvent<T1, T2> UpdateEvent { get; set; }
    //     IEasyEvent IUpdateAble.UpdateEvent => UpdateEvent;
    //     
    //     void Update(T1 t1, T2 t2)
    //     {
    //         OnUpdate();
    //         UpdateEvent.Invoke(t1, t2);
    //     }
    // }
}