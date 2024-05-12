using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public abstract class APointerEventListener : AUnityEventBinder<PointerEventData>, IEventListener
    {
    }
    
    public abstract class AUISelectEventListener : AUnityEventBinder<BaseEventData>, IEventListener
    {
    }

    public static class PointerEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : APointerEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<PointerEventData> action) where T : APointerEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : APointerEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<PointerEventData> action, GameObject go) where T : APointerEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<PointerEventData> action) where T : APointerEventListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : APointerEventListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class UISelectEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AUISelectEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<BaseEventData> action) where T : AUISelectEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AUISelectEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<BaseEventData> action, GameObject go) where T : AUISelectEventListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<BaseEventData> action) where T : AUISelectEventListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AUISelectEventListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => self.UnRegister());
            return self;
        }
    }
}