using System;

namespace EasyFramework
{
    public interface IDisposeAble:IDisposable
    {
        bool IsDispose { get; set; }
        IEasyEvent DisposeEvent { get; }
        
        void IDisposable.Dispose()=> Dispose();
        void Dispose(bool usePool=false)
        {
            if(!BeforeDisposeEvent(this, usePool)) return;
            DisposeEvent.BaseInvoke();
            AfterDisposeEvent(this, usePool);
        }
        protected void OnDispose(bool usePool);
        protected void DisposeDo(bool usePool) => EasyLifeCycle.DisposeDo.InvokeEvent(this);
        public static bool BeforeDisposeEvent(IDisposeAble self,bool usePool)
        {
            if (self.IsDispose) return false;

            if (self is IActiveAble activeAble)
                IActiveAble.ActiveAbleDispose(activeAble);
            else
                EasyLifeEx.DisableLifeCycle(self);
            self.IsDispose = true;
            self.OnDispose(usePool);
            return true;
        }
        public static void AfterDisposeEvent(IDisposeAble self,bool usePool)
        {
            self.DisposeDo(usePool);
            self.DisposeEvent.Clear();
            if (self is IInitAble initAble)
                initAble.InitEvent.Clear();
            if (self is IActiveAble activeAble)
            {
                activeAble.ActiveEvent.Clear();
                activeAble.UnActiveEvent.Clear();
            }

            if (self is IStartAble startAble)
            {
                startAble.IsStart = false;
                startAble.StartEvent.Clear();
            }
            if(self is IUpdateAble update)
                update.UpdateEvent.Clear();
            if(self is IFixedUpdateAble fixedUpdate)
                fixedUpdate.FixedUpdateEvent.Clear();
            
            if (usePool)
                ReferencePool.Recycle(self);
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