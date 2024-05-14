using System;
using EXFunctionKit;
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
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<PointerEventData> action) where T : APointerEventListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : APointerEventListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<PointerEventData> action, GameObject go) where T : APointerEventListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<PointerEventData> action) where T : APointerEventListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : APointerEventListener
        {
            go.Component<T>().Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class UISelectEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AUISelectEventListener
        {
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<BaseEventData> action) where T : AUISelectEventListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AUISelectEventListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<BaseEventData> action, GameObject go) where T : AUISelectEventListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<BaseEventData> action) where T : AUISelectEventListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AUISelectEventListener
        {
            go.Component<T>().Event.Register(_ => self.UnRegister());
            return self;
        }
    }
}