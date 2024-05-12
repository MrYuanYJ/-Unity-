using System;
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
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collision> action) where T : ACollisionListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : ACollisionListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collision> action, GameObject go) where T : ACollisionListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collision> action) where T : ACollisionListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }

        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : ACollisionListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_=>self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }

    public static class Collision2DEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : ACollision2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collision2D> action) where T : ACollision2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : ACollision2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collision2D> action, GameObject go) where T : ACollision2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collision2D> action) where T : ACollision2DListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : ACollision2DListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class TriggerEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AListenerListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collider> action) where T : AListenerListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AListenerListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collider> action, GameObject go) where T : AListenerListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collider> action) where T : AListenerListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AListenerListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
    public static class Trigger2DEventTriggerExtension
    {
        public static IUnRegisterHandle Register<T>(this GameObject self, Action action) where T : AListener2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(_ => action());
        }

        public static IUnRegisterHandle Register<T>(this GameObject self, Action<Collider2D> action) where T : AListener2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(self).Event.Register(action);
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action action, GameObject go) where T : AListener2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => action());
        }

        public static IUnRegisterHandle InvokeOn<T>(this Action<Collider2D> action, GameObject go) where T : AListener2DListener
        {
            return IEventListener.GetOrAddTrigger<T>(go).Event.Register(action);
        }

        public static void UnRegister<T>(this GameObject go, Action<Collider2D> action) where T : AListener2DListener
        {
            if (go.TryGetComponent<T>(out var monoEvent)) { monoEvent.Event.UnRegister(action); }
        }
        public static IUnRegisterHandle UnRegisterOn<T>(this IUnRegisterHandle self, GameObject go) where T : AListener2DListener
        {
            IEventListener.GetOrAddTrigger<T>(go).Event.Register(_ => self.UnRegister()).OnlyPlayOnce();
            return self;
        }
    }
}