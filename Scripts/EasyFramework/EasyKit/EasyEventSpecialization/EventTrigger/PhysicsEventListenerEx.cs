using System;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public abstract class ACollisionListener: AUnityEventBinder<Collision>, IEventListener
    {
    }
    public abstract class ACollision2DListener: AUnityEventBinder<Collision2D>, IEventListener
    {
    }
    public abstract class AListenerListener: AUnityEventBinder<Collider>, IEventListener
    {
    }
    public abstract class AListener2DListener: AUnityEventBinder<Collider2D>, IEventListener
    {
    }

    public static class CollisionEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : ACollisionListener
        {
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collision> action) where T : ACollisionListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : ACollisionListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collision> action, GameObject go) where T : ACollisionListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collision> action) where T : ACollisionListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : ACollisionListener
        {
            go.Component<T>().Event.Register(_=>self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }

    public static class Collision2DEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : ACollision2DListener
        {
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collision2D> action) where T : ACollision2DListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : ACollision2DListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collision2D> action, GameObject go) where T : ACollision2DListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collision2D> action) where T : ACollision2DListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : ACollision2DListener
        {
            go.Component<T>().Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class TriggerEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AListenerListener
        {
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collider> action) where T : AListenerListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AListenerListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collider> action, GameObject go) where T : AListenerListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collider> action) where T : AListenerListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AListenerListener
        {
            go.Component<T>().Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class Trigger2DEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AListener2DListener
        {
            return self.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collider2D> action) where T : AListener2DListener
        {
            return self.Component<T>().Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AListener2DListener
        {
            return go.Component<T>().Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collider2D> action, GameObject go) where T : AListener2DListener
        {
            return go.Component<T>().Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collider2D> action) where T : AListener2DListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AListener2DListener
        {
            go.Component<T>().Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
}