using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EasyECS
{
    public class EntityMgr
    {
        private static Dictionary<string, Type> AllType = new();
        public static void Init()
        {
            var selfAssembly = Assembly.GetAssembly( typeof( EntityMgr ) );
            Classify( selfAssembly );
            var assemblyNames = selfAssembly.GetReferencedAssemblies();
            foreach ( var assemblyName in assemblyNames )
            {
                var assembly = Assembly.Load( assemblyName.FullName );
                Classify( assembly );
            }

            ClassifySystem();
        }

        public static void Update()
        {
            var components = AllComponent.Values.ToArray();
            foreach ( var component in components )
            {
                
                component.TryUpdate();
            }
        }

        private static void Classify(Assembly assembly)
        {
            foreach ( var type in assembly.GetTypes() )
            {
                if ( type.IsAbstract || type.IsInterface )
                    continue;
                AllType.Add( type.AssemblyQualifiedName, type );

                if ( typeof( IComponentSystem ).IsAssignableFrom( type ) )
                {
                    AllSystem.Add( type, (IComponentSystem) Activator.CreateInstance( type ) );
                }
            }
        }

        private static void ClassifySystem()
        {
            foreach ( var system in AllSystem.Values )
            {
                if ( system is IAwakeSystm iAwakeSystm )
                {
                    AllAwakeSystem.Add( iAwakeSystm.GetComponentType(), iAwakeSystm );
                }
                if ( system is IUpdateSystm iUpdateSystm )
                {
                    AllUpdateSystem.Add( iUpdateSystm.GetComponentType(), iUpdateSystm );
                }
                if ( system is IDestroySystm iDestroySystem )
                {
                    AllDestroySystem.Add( iDestroySystem.GetComponentType(), iDestroySystem );
                }
            }
        }

        private static Dictionary<long, IComponent> AllComponent = new();
        private static Dictionary<long, IEntity> AllEntity = new();
        private static Dictionary<Type, IComponentSystem> AllSystem = new();
        private static Dictionary<Type, IAwakeSystm> AllAwakeSystem = new();
        private static Dictionary<Type, IUpdateSystm> AllUpdateSystem = new();
        private static Dictionary<Type, IDestroySystm> AllDestroySystem = new();

        private static long CurrentID;
        private static Stack<long> IDPool = new();

        private static long GetNewID() => CurrentID++;

        public static long FetchID()
        {
            if ( IDPool.Count == 0 )
            {
                return GetNewID();
            }
            else
            {
                return IDPool.Pop();
            }
        }

        public static void RecycleID(long id)
        {
            if ( IDPool.Count < 1024 )
                IDPool.Push( id );
        }

        public static IEntity GetEntity(long entityID) => AllEntity[entityID];
        public static T GetEntity<T>(long entityID) where T : IEntity => (T) AllEntity[entityID];
        public static IComponent GetComponent(long componentID) => AllComponent[componentID];
        public static T GetComponent<T>(long componentID) where T : IComponent => (T) AllComponent[componentID];

        public static IComponent CreateComponent(Type type)
        {
            var component = Activator.CreateInstance( type ) as IComponent;
            if ( component == null ) return default;

            component.ID = FetchID();
            AllComponent.Add( component.ID, component );
#if UNITY_EDITOR
            
#endif
            return component;
        }

        public static IEntity CreateEntity(Type type)
        {
            if ( typeof( MonoBehaviour ).IsAssignableFrom( type ) )
                throw new Exception( $"This Method Can Not Create MonoBehaviour." );
            else
            {
                var entity = Activator.CreateInstance( type ) as IEntity;
                if ( entity == null ) return default;

                AddEntityToDic( entity );
                return entity;
            }
        }
        public static T CreateEntity<T>() where T: IEntity
        {
            return (T) CreateEntity(typeof(T));
        }

        private static bool CheckBase(IComponent self)
        {
            if ( self.Parent != null )
            {
                if ( ( self.Parent as IDispose ).isDispose )
                {
                    self.Dispose();
                    return false;
                }
            }
            else if ( self.ChildContainer != null )
            {
                if ( ( self.ChildContainer as IDispose ).isDispose )
                {
                    self.Dispose();
                    return false;
                }
            }

            return true;
        }

        public static void TryAwake(IComponent self)
        {
            if ( !CheckBase( self ) ) return;

            if ( AllAwakeSystem.TryGetValue( self.GetType(), out var system ) )
            {
                system.Awake( self );
            }
        }

        public static void TryUpdate(IComponent self)
        {
            if ( !CheckBase( self ) ) return;

            if ( AllUpdateSystem.TryGetValue( self.GetType(), out var system ) )
            {
                system.Update( self );
            }
        }
        public static void TryDestroy(IComponent self)
        {
            if ( AllDestroySystem.TryGetValue( self.GetType(), out var system ) )
            {
                system.Destroy( self );
            }
        }

        public static void AddEntityToDic(IEntity entity)
        {
            if ( !AllEntity.ContainsKey( entity.EntityID ) || AllEntity[entity.EntityID] != entity )
            {
                entity.EntityID = FetchID();
                AllEntity.Add( entity.EntityID, entity );
                entity.Init();
            }
        }

        public static T GetNewMonoEntity<T>(GameObject go) where T : Component, IEntity
        {
            return go.AddComponent<T>();
        }
        public static void RemoveComponent(long componentID)
        {
            AllComponent.Remove( componentID );
        }

        public static void RemoveEntity(long entityID)
        {
            AllEntity.Remove( entityID );
        }

        public static void DisposeComponent(long componentID)
        {
            if ( AllComponent.TryGetValue( componentID, out var component ) )
            {
                component.Dispose();
            }
        }
        public static void DisposeEntity(long entityID)
        {
            if ( AllEntity.TryGetValue( entityID, out var entity ) )
            {
                entity.Dispose();
            }
        }
    }

    public abstract class EventName
    {

    }
}