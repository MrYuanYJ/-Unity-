using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IDisposeAble:IDisposable
    {
        bool IsDispose { get; set; }
        IEasyEvent DisposeEvent { get; }
        
        void IDisposable.Dispose()=> Dispose();
        void Dispose(bool usePool=false)
        {
            if(!BeforeDisposeEvent(this)) return;
            DisposeEvent.Invoke();
            AfterDisposeEvent(this, usePool);
        }
        void OnDispose();
        void DisposeDo() => GlobalEvent.DisposeDo.InvokeEvent(this);
        public static bool BeforeDisposeEvent(IDisposeAble self)
        {
            if (self.IsDispose) return false;
            self.IsDispose = true;
            self.OnDispose();
            return true;
        }
        public static void AfterDisposeEvent(IDisposeAble self,bool usePool)
        {
            self.DisposeDo();
            self.DisposeEvent.Clear();
            if(self is IInitAble initAble)
                initAble.InitEvent.Clear();
            if(self is IUpdateAble update)
                update.UpdateEvent.Clear();
            if(self is IFixedUpdateAble fixedUpdate)
                fixedUpdate.FixedUpdateEvent.Clear();
            if (usePool)
                GlobalEvent.RecycleClass.InvokeEvent(self);
        }
    }

    // public interface IDisposeAble<T> : IDisposeAble
    // {
    //     new EasyEvent<T> DisposeEvent { get; set; }
    //     IEasyEvent IDisposeAble.DisposeEvent=> DisposeEvent;
    //
    //     void Dispose(T t, bool usePool)
    //     {
    //         if(!BeforeDisposeEvent(this)) return;
    //         DisposeEvent.Invoke(t);
    //         AfterDisposeEvent(this, usePool);
    //     }
    // }
    // public interface IDisposeAble<T1, T2> : IDisposeAble
    // {
    //     new EasyEvent<T1, T2> DisposeEvent { get; set; }
    //     IEasyEvent IDisposeAble.DisposeEvent => DisposeEvent;
    //
    //     void Dispose(T1 t1, T2 t2, bool usePool)
    //     {
    //         if (!BeforeDisposeEvent(this)) return;
    //         DisposeEvent.Invoke(t1, t2);
    //         AfterDisposeEvent(this, usePool);
    //     }
    // }
}