using System;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public interface IEventListener
    {

    }
    public abstract class AMonoListener : AUnityEventBinder,IEventListener
    {
    }

    public static class MonoEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T: AMonoListener
        {
            return self.Component<T>().Event.Register(action);
        }
        public static IUnRegisterHandle RegisterOnDestroy(this GameObject self, Action action)
        {
            return self.Component<DestroyListener>().Event.Register(action).OnlyPlayOnce();
        }
        
        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AMonoListener
        {
            return go.Component<T>().Event.Register(action);
        }
        public static IUnRegisterHandle InvokeOnDestroy(this Action action, GameObject go)
        {
            return go.Component<DestroyListener>().Event.Register(action).OnlyPlayOnce();
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
            go.Component<T>().Event.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnDestroy(this IUnRegisterHandle self,GameObject go)
        { 
            go.Component<DestroyListener>().Event.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
    }
}