using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyECS
{
    public abstract class AbstractContainer : IComponentContainer, IChildComponentContainer
    {
        public bool isDispose { get; set; }
        public Dictionary<Type, IComponent> Components { get; set; } = new();
        public Dictionary<long, IComponent> Children { get; set; } = new();

        public T AddOrGetComponent<T>() where T : IComponent
        {
            return (T) AddOrGetComponent( typeof( T ) );
        }

        public IComponent AddOrGetComponent(Type type)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                return component;
            }

            component = EntityMgr.CreateComponent( type );
            component.TobeComponent( this );
            return component;
        }

        public T GetComponent<T>(Action<T> callBack) where T : IComponent
        {
            if ( Components.TryGetValue( typeof( T ), out var component ) )
            {
                var tComponent = (T) component;
                callBack?.Invoke( tComponent );
                return tComponent;
            }

            return default;
        }

        public IComponent GetComponent(Type type, Action<IComponent> callBack)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                callBack?.Invoke( component );
                return component;
            }

            return default;
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            RemoveComponent( typeof( T ) );
        }

        public void RemoveComponent(Type type)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                component.Dispose();
            }
        }

        public void RemoveAllComponent()
        {
            var components = Components.Values.ToArray();
            foreach ( var component in components )
            {
                component.Dispose();
            }
        }

        public T AddChild<T>() where T : IComponent
        {
            return (T) AddChild( typeof( T ) );
        }

        public IComponent AddChild(Type type)
        {
            var component = EntityMgr.CreateComponent( type );
            component.TobeChild( this );
            return component;
        }

        public T GetChild<T>(long id, Action<T> callBack) where T : IComponent
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                var tComponent = (T) component;
                callBack?.Invoke( tComponent );
                return tComponent;
            }

            return default;
        }

        public IComponent GetChild(long id, Action<IComponent> callBack)
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                callBack?.Invoke( component );
                return component;
            }

            return default;
        }

        public void RemoveChild(long id)
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                component.Dispose();
            }
        }

        public void RemoveAllChild()
        {
            var components = Children.Values.ToArray();
            foreach ( var component in components )
            {
                component.Dispose();
            }
        }
    }
    public abstract class AMonobstractContainer : MonoBehaviour, IComponentContainer, IChildComponentContainer
    {
        public Dictionary<Type, IComponent> Components { get; set; } = new();
        public Dictionary<long, IComponent> Children { get; set; } = new();

        public T AddOrGetComponent<T>() where T : IComponent
        {
            return (T) AddOrGetComponent( typeof( T ) );
        }

        public IComponent AddOrGetComponent(Type type)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                return component;
            }

            component = EntityMgr.CreateComponent( type );
            component.TobeComponent( this );
            return component;
        }

        public T GetComponent<T>(Action<T> callBack) where T : IComponent
        {
            if ( Components.TryGetValue( typeof( T ), out var component ) )
            {
                var tComponent = (T) component;
                callBack?.Invoke( tComponent );
                return tComponent;
            }

            return default;
        }

        public IComponent GetComponent(Type type, Action<IComponent> callBack)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                callBack?.Invoke( component );
                return component;
            }

            return default;
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            RemoveComponent( typeof( T ) );
        }

        public void RemoveComponent(Type type)
        {
            if ( Components.TryGetValue( type, out var component ) )
            {
                component.Dispose();
            }
        }

        public void RemoveAllComponent()
        {
            var components = Components.Values.ToArray();
            foreach ( var component in components )
            {
                component.Dispose();
            }
        }

        public T AddChild<T>() where T : IComponent
        {
            return (T) AddChild( typeof( T ) );
        }

        public IComponent AddChild(Type type)
        {
            var component = EntityMgr.CreateComponent( type );
            component.TobeChild( this );
            return component;
        }

        public T GetChild<T>(long id, Action<T> callBack) where T : IComponent
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                var tComponent = (T) component;
                callBack?.Invoke( tComponent );
                return tComponent;
            }

            return default;
        }

        public IComponent GetChild(long id, Action<IComponent> callBack)
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                callBack?.Invoke( component );
                return component;
            }

            return default;
        }

        public void RemoveChild(long id)
        {
            if ( Children.TryGetValue( id, out var component ) )
            {
                component.Dispose();
            }
        }

        public void RemoveAllChild()
        {
            var components = Children.Values.ToArray();
            foreach ( var component in components )
            {
                component.Dispose();
            }
        }
    }
}