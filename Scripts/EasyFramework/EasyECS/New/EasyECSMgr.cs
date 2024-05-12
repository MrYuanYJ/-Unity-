using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyECS.New
{
    public class EasyECSMgr
    {
        private static long NextComponentID;
        public static Dictionary<Type, IArrayContainer> AllEasyComponentDic = new();
        private static Dictionary<Type, Queue<long>> EasyComponentDisposeDic = new();
        private static long NextEntityID;
        private static Dictionary<long,IEasyEntity> AllEasyEntityDic = new ();
        public static Dictionary<Type, IEasyComponentSystem> AllEasySystemDic = new();

        public static event Action OnUpdate;
        public static event Action OnFixedUpdate;

        public static void Init()
        {
            var systems = typeof(IEasyComponentSystem).Assembly.GetTypes()
                .Where(x => !x.IsInterface&&!x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(IEasyComponentSystem).IsAssignableFrom(x));
            foreach (var system in systems)
            {
                var easyComponentSystem = (IEasyComponentSystem) Activator.CreateInstance(system);
                AllEasySystemDic.Add(easyComponentSystem.ComponentType,easyComponentSystem);
            }
        }

        public static void Update() => OnUpdate?.Invoke();
        public static void FixedUpdate() => OnFixedUpdate?.Invoke();

        public static IEasyComponentSystem GetSystem<T>() where T : struct, IEasyComponent => AllEasySystemDic[typeof(T)];
        public static IEasyComponentSystem GetSystem(Type componentType) => AllEasySystemDic[componentType];

        public static ref T GetEasyComponent<T>(long id)where T: struct,IEasyComponent
        {
            return ref AllEasyComponentDic.Get<T>(id);
        }

        public static IArrayContainer<T> GetEasyComponents<T>(bool ignoreDispose=true) where T: struct,IEasyComponent
        {
            if (!AllEasyComponentDic.ContainsKey(typeof(T)))
            {
                AllEasyComponentDic.Add(typeof(T), new ArrayContainer<T>());
            }
            return (IArrayContainer<T>) AllEasyComponentDic[typeof(T)];
        }
        

        public static ref T CreateEasyComponent<T>() where T: struct,IEasyComponent
        {
            if (EasyComponentDisposeDic.TryGetValue(typeof(T), out var que) && que.Count > 0)
            {
               ref var easyComponent= ref AllEasyComponentDic[typeof(T)].Get<T>(que.Dequeue());
               easyComponent.IsDispose = false;
               ((IEasyComponentSystem<T>)GetSystem<T>().Init()).OnComponentAwake(ref easyComponent);
               return ref easyComponent;
            }

            ref var component= ref AllEasyComponentDic.Add(new T());
            ((IEasyComponentSystem<T>)GetSystem<T>().Init()).OnComponentAwake(ref component);
            return ref component;
        } 
        
        public static void EasyComponentDispose(Type type,long uniqueId)
        {
            if (AllEasyComponentDic.TryGetValue(type, out var iArrayContainer))
            {
                iArrayContainer.DisposeComponent(uniqueId);
            }
        }

        public static void DisposeComponent<T>(ref T component) where T : struct,IEasyComponent
        {
            if ( component.IsDispose ) return;
        
            component.IsDispose = true;
            var type = typeof(T);
            if (component.Entity != null)
            {
                component.Entity = null;
            }
            EasyECSMgr.AddToComponentDisposeDic(type,component.ID);
            var system = (IEasyComponentSystem<T>) GetSystem<T>();
            system.OnComponentDestroy(ref component);
            if (AllEasyComponentDic[type].OccupyCount == EasyComponentDisposeDic[type].Count)
            {
                system.Destroy();
            }
        }

        public static void AddToComponentDisposeDic(Type type, long uniqueId)
        {
            if (!EasyComponentDisposeDic.ContainsKey(type))
            {
                EasyComponentDisposeDic.Add(type, new());
            }

            EasyComponentDisposeDic[type].Enqueue(uniqueId);
        }


        public static T GetEasyEntity<T>(long id)
        {
            return (T)AllEasyEntityDic[id];
        }
        public static IEasyEntity GetEasyEntity(long id)
        {
            return AllEasyEntityDic[id];
        }
        
        public static T CreateEasyEntity<T>() where T: IEasyEntity
        {
            return (T) CreateEasyEntity(typeof(T));
        }

        public static IEasyEntity CreateMonoEasyEntity(System.Type type,GameObject parent)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                IEasyEntity entity = (IEasyEntity)parent.AddComponent(type);
                
                entity.ID=NextEntityID;

                AllEasyEntityDic[NextEntityID] = entity;
                NextEntityID++;
                return entity;
            }

            return null;
        }
        public static IEasyEntity CreateEasyEntity(System.Type type)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
                return null;
            if (typeof(IEasyEntity).IsAssignableFrom(type))
            {
                var entity = (IEasyEntity) System.Activator.CreateInstance(type);

                entity.ID=NextEntityID;

                AllEasyEntityDic[NextEntityID] = entity;
                NextEntityID++;
                return entity;

            }

            return null;
        }
        public static void EasyEntityDispose(IEasyEntity easyEntity)
        {
            AllEasyEntityDic.Remove(easyEntity.ID);
        }
    }
    
    public static class EasyECSMgrEX
    {
        public static ref T Add<T>(this Dictionary<Type, IArrayContainer> self,T newComponent) where T : struct, IEasyComponent
        {
            if (self.TryGetValue(typeof(T),out var iArrayContainer))
            {
                return ref self.Get<T>(((IArrayContainer<T>)iArrayContainer).Add(newComponent));
            }

            iArrayContainer = new ArrayContainer<T>();
            var id=iArrayContainer.Add(newComponent);
            self.Add(typeof(T),iArrayContainer);
            
            ref var component = ref self.Get<T>(id);
            component.ID=id;

            return ref component;
        }

        public static ref T Get<T>(this Dictionary<Type, IArrayContainer> self, long uniqueId)
        {
            return ref self[typeof(T)].Get<T>(uniqueId);
        }
    }
}