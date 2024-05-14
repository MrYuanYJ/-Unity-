using System;
using System.Collections.Generic;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public class EasyMonoOnInitDoEvent : AutoClassEvent<GlobalEvent.InitDo,object>
    {
        protected override void Run(object a)
        {
            EasyMono.Instance.TryRegister(a);
        }
    }
    
    public class EasyMonoOnDisposeDoEvent : AutoClassEvent<GlobalEvent.DisposeDo,object>
    {
        protected override void Run(object a)
        {
            EasyMono.Get()?.TryUnRegister(a);
        }
    }
    public class EasyMono: AutoMonoSingleton<EasyMono>
    {
        readonly EasyEvent _updateEvent = new();
        readonly EasyEvent _fixedUpdateEvent= new();

        private void Update()
        {
            _updateEvent.Invoke();
        }

        private void FixedUpdate()=> _fixedUpdateEvent.Invoke();

        public void TryRegister(object obj)
        {
            if (obj is IStartAble start)
                _updateEvent.Register(start.Start).OnlyPlayOnce();
            if (obj is IUpdateAble update)
                _updateEvent.RegisterAfterInvoke(update.Update);
            if (obj is IFixedUpdateAble fixedUpdate)
                _fixedUpdateEvent.Register(fixedUpdate.FixedUpdate);
        }

        public void TryUnRegister(object obj)
        {
            if (obj is IUpdateAble update)
                _updateEvent.UnRegister(update.Update);
            if (obj is IFixedUpdateAble fixedUpdate)
                _fixedUpdateEvent.UnRegister(fixedUpdate.FixedUpdate);
        }


        public static IUnRegisterHandle Register<T>(Action action) where T: AMonoListener =>
            Instance.gameObject.Register<T>(action);

        public static void UnRegister<T>(Action action) where T: AMonoListener =>
            Get()?.gameObject.UnRegister<T>(action);
    }
}