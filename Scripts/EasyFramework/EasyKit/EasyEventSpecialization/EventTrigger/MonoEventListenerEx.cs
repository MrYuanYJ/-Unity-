using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public interface IEventListener
    {
        public static T GetOrAddTrigger<T>(GameObject self) where T: Component, IEventListener
        {
            if (!self.TryGetComponent<T>(out var monoEvent))
            {
                monoEvent=self.AddComponent<T>();
            }

            return monoEvent; 
        }
    }
    public abstract class AMonoListener : AUnityEventBinder,IEventListener
    {
    }

    public static class MonoEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T: AMonoListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }
        public static IUnRegisterHandle RegisterOnDestroy(this GameObject self, Action action)
        {
            return IEventListener.GetOrAddTrigger<DestroyListener>(self).Event.Register(action).OnlyPlayOnce();
        }
        
        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AMonoListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }
        public static IUnRegisterHandle InvokeOnDestroy(this Action action, GameObject go)
        {
            return IEventListener.GetOrAddTrigger<DestroyListener>(go).Event.Register(action).OnlyPlayOnce();
        }

        public static void UnRegister<T>(this GameObject go,Action action) where T : AMonoListener
        {
            if (go.TryGetComponent<T>(out var monoEvent))
            {
                monoEvent.Event.UnRegister(action);
            }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self,GameObject go) where T : AMonoListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnDestroy(this IUnRegisterHandle self,GameObject go)
        { 
            IEventListener.GetOrAddTrigger<DestroyListener>(go).Event.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
    }
}