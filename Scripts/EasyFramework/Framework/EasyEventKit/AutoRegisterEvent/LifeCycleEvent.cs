using System;

namespace EasyFramework.EventKit
{
    public interface IAutoRegisterLifeCycleEvent
    {
        Type RegisterType { get; }
        IUnRegisterHandle Register(IEasyLife arg);
    }

    public abstract class InitAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnInit(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => self.InitEvent
                .Register(() => OnInit((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class StartAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife,IStartAble
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnStart(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).StartEvent
                .Register(() => OnStart((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class DisposeAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnInit(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => self.DisposeEvent
                .Register(() => OnInit((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class UpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IUpdateAble
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnUpdate(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).UpdateEvent
                .Register(() => OnUpdate((T)self))
                .UnRegisterOnDispose(self);
    }

    public abstract class FixedUpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IFixedUpdateAble
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnFixedUpdate(T self);

        public IUnRegisterHandle Register(IEasyLife self)
            => ((T)self).FixedUpdateEvent
                .Register(() => OnFixedUpdate((T)self))
                .UnRegisterOnDispose(self);
    }
}