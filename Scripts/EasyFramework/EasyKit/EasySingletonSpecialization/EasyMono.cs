using System;
using System.Collections.Generic;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public class EasyMonoOnInitDoEvent : AutoClassEvent<GlobalEvent.InitDo,IInitAble>
    {
        protected override void Run(IInitAble a)
        {
            EasyMono.Instance.TryRegister(a);
        }
    }
    
    public class EasyMonoOnDisposeDoEvent : AutoClassEvent<GlobalEvent.DisposeDo,IDisposeAble>
    {
        protected override void Run(IDisposeAble a)
        {
            EasyMono.Get()?.TryUnRegister(a);
        }
    }
    public class EasyMono: AutoMonoSingleton<EasyMono>
    {
        private readonly Queue<Action> _set = new();
        readonly EasyEvent _updateEvent = new();
        readonly EasyEvent _fixedUpdateEvent= new();

        private void Update()
        {
            _updateEvent.Invoke();
            while (_set.Count > 0)
                _set.Dequeue()();
        }

        private void FixedUpdate()=> _fixedUpdateEvent.Invoke();

        public void TryRegister(IInitAble obj)
        {
            if (obj is IStartAble start)
                _set.Enqueue(()=>_updateEvent.Register(start.Start).OnlyPlayOnce());
            if (obj is IEasyUpdate update)
                _set.Enqueue(()=>_updateEvent.RegisterAfterInvoke(update.Update));
            if (obj is IEasyFixedUpdate fixedUpdate)
                _set.Enqueue(()=>_fixedUpdateEvent.Register(fixedUpdate.FixedUpdate));
        }

        public void TryUnRegister(IDisposeAble obj)
        {
            if (obj is IEasyUpdate update)
                _updateEvent.UnRegister(update.Update);
            if (obj is IEasyFixedUpdate fixedUpdate)
                _fixedUpdateEvent.UnRegister(fixedUpdate.FixedUpdate);
        }


        public static IUnRegisterHandle Register<T>(Action action) where T: AMonoListener =>
            Instance.gameObject.Register<T>(action);

        public static void UnRegister<T>(Action action) where T: AMonoListener =>
            Get()?.gameObject.UnRegister<T>(action);
    }
}