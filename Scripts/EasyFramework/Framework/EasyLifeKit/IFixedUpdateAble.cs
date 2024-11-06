

namespace EasyFramework
{
    public interface IFixedUpdateAble : IEasyLife
    {
        IEasyEvent FixedUpdateEvent { get; }
        
        static void EnableFixedUpdateAble(IFixedUpdateAble fixedUpdateAble)=>EasyLifeCycle.FixedUpdate.RegisterEvent(fixedUpdateAble.FixedUpdate);
        static void DisableFixedUpdateAble(IFixedUpdateAble fixedUpdateAble)=>EasyLifeCycle.FixedUpdate.UnRegisterEvent(fixedUpdateAble.FixedUpdate);
        void FixedUpdate()
        {
            OnFixedUpdate();
            FixedUpdateEvent.BaseInvoke();
        }
        protected void OnFixedUpdate();
    }

    // public interface IFixedUpdateAble<T> : IFixedUpdateAble
    // {
    //     new EasyEvent<T> FixedUpdateEvent { get; set; }
    //     IEasyEvent IFixedUpdateAble.FixedUpdateEvent => FixedUpdateEvent;
    //
    //     void FixedUpdate(T t)
    //     {
    //         OnFixedUpdate();
    //         FixedUpdateEvent.Invoke(t);
    //     }
    // }
    // public interface IFixedUpdateAble<T1, T2> : IFixedUpdateAble
    // {
    //     new EasyEvent<T1, T2> FixedUpdateEvent { get; set; }
    //     IEasyEvent IFixedUpdateAble.FixedUpdateEvent => FixedUpdateEvent;
    //
    //     void FixedUpdate(T1 t1, T2 t2)
    //     {
    //         OnFixedUpdate();
    //         FixedUpdateEvent.Invoke(t1, t2);
    //     }
    // }
}