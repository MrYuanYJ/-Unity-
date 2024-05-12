using System;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public class EasyMonoOnInitDoEvent : AutoClassEvent<GlobalEvent.InitDo,IEasyLife>
    {
        protected override void Run(IEasyLife a)
        {
            EasyMono.TryRegister(a);
        }
    }
    
    public class EasyMonoOnDisposeDoEvent : AutoClassEvent<GlobalEvent.DisposeDo,IEasyLife>
    {
        protected override void Run(IEasyLife a)
        {
            EasyMono.TryUnRegister(a);
        }
    }
    public class EasyMono: AutoMonoSingleton<EasyMono>
    {
        public static void TryRegister(IEasyLife obj)
        {
            if (!(obj is MonoBehaviour))
            {
                Register<UpdateListener>(obj.Start).OnlyPlayOnce();
                if (obj is IEasyUpdate update)
                    Register<UpdateListener>(update.Update);
                if (obj is IEasyFixedUpdate fixedUpdate)
                    Register<FixedUpdateListener>(fixedUpdate.FixedUpdate);
            }
        }
        
        public static void TryUnRegister(IEasyLife obj)
        {
            if (!(obj is MonoBehaviour))
            {
                if (obj is IEasyUpdate update)
                    UnRegister<UpdateListener>(update.Update);
                if (obj is IEasyFixedUpdate fixedUpdate)
                    UnRegister<FixedUpdateListener>(fixedUpdate.FixedUpdate);
            }
        }
        
        
        public static IUnRegisterHandle Register<T>(Action action) where T: AMonoListener =>
            Instance.gameObject.Register<T>(action);

        public static void UnRegister<T>(Action action) where T: AMonoListener =>
            Get()?.gameObject.UnRegister<T>(action);
    }
}