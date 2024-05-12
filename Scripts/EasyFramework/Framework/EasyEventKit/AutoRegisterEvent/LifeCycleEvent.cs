using System;

namespace EasyFramework.EventKit
{
    public interface IAutoRegisterLifeCycleEvent
    {
        Type RegisterType { get; }
        void Register(IEasyLife arg);
    }

    public abstract class InitAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnInit(T self);

        public void Register(IEasyLife self)
            => self.InitEvent
                .Register(target => OnInit((T)target))
                .UnRegisterOnDispose(self);
    }

    public abstract class StartAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnStart(T self);

        public void Register(IEasyLife self)
            => self.StartEvent
                .Register(target => OnStart((T)target))
                .UnRegisterOnDispose(self);
    }

    public abstract class DisposeAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyLife
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnInit(T self);

        public void Register(IEasyLife self)
            => self.DisposeEvent
                .Register(target => OnInit((T)target))
                .UnRegisterOnDispose(self);
    }

    public abstract class UpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyUpdate
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnUpdate(T self);

        public void Register(IEasyLife self)
            => ((T)self).UpdateEvent
                .Register(target => OnUpdate((T)target))
                .UnRegisterOnDispose(self);
    }

    public abstract class FixedUpdateAutoEvent<T> : IAutoRegisterLifeCycleEvent where T : IEasyFixedUpdate
    {
        public Type RegisterType => typeof(T);
        protected abstract void OnFixedUpdate(T self);

        public void Register(IEasyLife self)
            => ((T)self).FixedUpdateEvent
                .Register(target => OnFixedUpdate((T)target))
                .UnRegisterOnDispose(self);
    }
}