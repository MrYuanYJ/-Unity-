using System;

namespace EasyFramework.EventKit
{
    public interface IAutoRegisterLifeCycleEvent
    {
        Type RegisterType { get; }
        IUnRegisterHandle Register(IEasyLife arg);
    }

    public interface IAutoRegisterLifeCycleEvent<T>: IAutoRegisterLifeCycleEvent
    {
        Type IAutoRegisterLifeCycleEvent.RegisterType => typeof(T);
    }

    public abstract class InitAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IEasyLife
    {
        protected abstract void OnInit(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => self.InitEvent
                .Register(() => OnInit((T)self))
                .UnRegisterOnDispose(self);
    }
    public abstract class ActiveAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IActiveAble
    {
        protected abstract void OnActive(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).ActiveEvent
                .Register(() => OnActive((T)self))
                .UnRegisterOnDispose(self);
    }
    public abstract class UnActiveAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IActiveAble
    {
        protected abstract void OnUnActive(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).UnActiveEvent
                .Register(() => OnUnActive((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class StartAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IEasyLife,IStartAble
    {
        protected abstract void OnStart(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).StartEvent
                .Register(() => OnStart((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class DisposeAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IEasyLife
    {
        protected abstract void OnInit(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => self.DisposeEvent
                .Register(() => OnInit((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class UpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IUpdateAble
    {
        protected abstract void OnUpdate(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).UpdateEvent
                .Register(() => OnUpdate((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class FixedUpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent<T> where T : IFixedUpdateAble
    {
        protected abstract void OnFixedUpdate(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).FixedUpdateEvent
                .Register(() => OnFixedUpdate((T)self))
                .UnRegisterOnDispose(self);
    }
}