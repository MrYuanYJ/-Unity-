using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyECS
{
    public interface IDispose : IDisposable
    {
        bool isDispose { get; set; }
    }
    public interface IComponentContainer
    {
        Dictionary<Type, IComponent> Components { get; set; }

        T AddOrGetComponent<T>() where T : IComponent;
        IComponent AddOrGetComponent(Type type);
        T GetComponent<T>(Action<T> callBack) where T : IComponent;
        IComponent GetComponent(Type type, Action<IComponent> callBack);
        void RemoveComponent<T>() where T : IComponent;
        void RemoveComponent(Type type);
        void RemoveAllComponent();
    }

    public interface IChildComponentContainer
    {
        Dictionary<long, IComponent> Children { get; set; }

        T AddChild<T>() where T : IComponent;
        IComponent AddChild(Type type);
        T GetChild<T>(long id, Action<T> callBack) where T : IComponent;
        IComponent GetChild(long id, Action<IComponent> callBack);
        void RemoveChild(long id);
        void RemoveAllChild();
    }


    public interface IUpdateable
    {
        void Update();
    }
    public interface IFixedUpdateable
    {
        void FixedUpdate();
    }
    public interface IDestroyable
    {
        void Destroy();
    }


    public interface IEntity :
        IComponentContainer,
        IChildComponentContainer,
        IDispose
    {
        long EntityID { get; set; }
        Action OnDisposed { get; set; }
        void Init();
        void IDisposable.Dispose()
        {
            if ( isDispose ) return;

            isDispose = true;
            OnDispose();
            OnDisposed?.Invoke();
            OnDisposed = null;
            RemoveAllChild();
            RemoveAllComponent();
            EntityMgr.RecycleID( EntityID );
            EntityMgr.RemoveEntity( EntityID );
            if ( this is MonoBehaviour mono )
                GameObject.Destroy( mono );
        }

        void OnDispose();
    }

    public static class IEntityExtension
    {
        public static void InvokeOnDispose(this Action action, IEntity entity)
        {
            entity.OnDisposed += action;
        }

        public static (T RegisterEvent, Action UnRegisterEvent) UnRegisterOnDispose<T>(this (T _, Action UnRegisterEvent) self,IEntity entity)
        {
            entity.OnDisposed += self.UnRegisterEvent;
            return self;
        }
    }

    public interface IComponent :
        IComponentContainer,
        IChildComponentContainer,
        IDispose
    {
        long ID { get; set; }
        IEntity Entity { get; set; }
        IComponentContainer Parent { get; set; }
        IChildComponentContainer ChildContainer { get; set; }
        Action OnDisposed { get; set; }

        public void TobeComponent(IComponentContainer parent)
        {
            if ( parent is IEntity entity )
                Entity = entity;
            else if ( parent is IComponent component )
                Entity = component.Entity;
            Parent = parent;
            parent.Components.Add( this.GetType(), this );
            EntityMgr.TryAwake( this );
        }
        public void TobeChild(IChildComponentContainer childContainer)
        {
            if ( childContainer is IEntity entity )
                Entity = entity;
            else if ( childContainer is IComponent component )
                Entity = component.Entity;
            ChildContainer = childContainer;
            childContainer.Children.Add( this.ID, this );
            EntityMgr.TryAwake( this );
        }

        public void TryUpdate() => EntityMgr.TryUpdate( this );

        public void TryDestroy() => EntityMgr.TryDestroy( this );
        T GetParent<T>(Action<T> callBack) where T : IComponentContainer;
        T GetChildContainer<T>(Action<T> callBack) where T : IChildComponentContainer;

        void IDisposable.Dispose()
        {
            if ( isDispose ) return;

            isDispose = true;
            this.TryDestroy();
            OnDisposed?.Invoke();
            OnDisposed = null;
            if ( Parent != null )
            {
                Parent.Components.Remove( this.GetType() );
                Parent = null;
            }

            if ( ChildContainer != null )
            {
                ChildContainer.Children.Remove( this.ID );
                ChildContainer = null;
            }
            RemoveAllChild();
            RemoveAllComponent();
            EntityMgr.RecycleID( ID );
            EntityMgr.RemoveComponent( ID );
        }
    }

    public static class IComponentExtension
    {
        public static void InvokeOnDispose(this Action action, IComponent component)
        {
            component.OnDisposed += action;
        }
        public static (T RegisterEvent, Action UnRegisterEvent) UnRegisterOnDispose<T>(this (T _, Action UnRegisterEvent) self,IComponent component)
        {
            component.OnDisposed += self.UnRegisterEvent;
            return self;
        }
    }
    public interface IComponentSystem
    {
        public Type GetComponentType() => this.GetType();
    }

    public interface IAwakeSystm : IComponentSystem
    {
        public void Awake(IComponent self) { }
    }

    public interface IUpdateSystm : IComponentSystem
    {
        public void Update(IComponent self) { }
    }

    public interface IDestroySystm : IComponentSystem
    {
        public void Destroy(IComponent self) { }
    }
    public interface IAwakeSystm<Component> : IAwakeSystm where Component : IComponent
    {
        void IAwakeSystm.Awake(IComponent self) => Awake( (Component) self );
        void Awake(Component self);
    }
    public interface IUpdateSystm<Component> : IUpdateSystm where Component : IComponent
    {
        void IUpdateSystm.Update(IComponent self) => Update( (Component) self );
        void Update(Component self);
    }
    public interface IDestroySystm<Component> : IDestroySystm where Component : IComponent
    {
        void IDestroySystm.Destroy(IComponent self) => Destroy( (Component) self );
        void Destroy(Component self);
    }
}