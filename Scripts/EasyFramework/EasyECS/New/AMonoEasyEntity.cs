using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyECS.New
{
    public abstract class AMonoEasyEntity : MonoBehaviour, IEasyEntity
    {
        private bool _isDispose;
        private long _id;
        private bool _isActive;

        public Dictionary<Type,List<long>> Children { get; protected set; } = new();
        public Dictionary<Type, long> Components { get; protected set; } = new();

        long IUnique.ID
        {
            get => _id;
            set => _id = value;
        }

        bool IDispose.IsDispose
        {
            get => _isDispose;
            set => _isDispose = value;
        }

        bool IActivatable.IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public ref T AddChild<T>() where T : struct, IEasyComponent
        {
           ref var component= ref EasyECSMgr.CreateEasyComponent<T>();
           component.Entity = this;
           if (!Children.TryGetValue(typeof(T), out var lst))
           {
               lst = new List<long>();
               Children.Add(typeof(T),lst);
           }
           lst.Add(component.ID);
           return ref component;
        }


        public ref T GetChild<T>(long id) where T : struct, IEasyComponent
        {
            if (Children[typeof(T)].Contains(id))
            {
                return ref EasyECSMgr.GetEasyComponent<T>(id);
            }

            throw new Exception($"Can Not Find Child [{typeof(T)}]: {id}");
        }

        public void RemoveChild<T>(long id) where T : struct, IEasyComponent
        {
            RemoveChild(typeof(T),id);
        }

        public void RemoveChild(Type type, long id)
        {
            EasyECSMgr.EasyComponentDispose(type,id);
            Children[type].Remove(id);
        }

        public void RemoveAllChild()
        {
            foreach (var item in Children)
            {
                foreach (var id in item.Value)
                {
                    EasyECSMgr.EasyComponentDispose(item.Key, id);
                }
            }
            Children.Clear();
        }

        public ref T AddEasyComponent<T>() where T : struct, IEasyComponent
        {
            if (Components.TryGetValue(typeof(T), out var id))
            {
                throw new Exception($"Can Not Add Same Component [{typeof(T)}]: {id}");
            }
            ref var component = ref EasyECSMgr.CreateEasyComponent<T>();
            component.Entity = this;
            Components.Add(typeof(T),component.ID);
            return ref component;
        }

        public ref T GetEasyComponent<T>() where T : struct, IEasyComponent
        {
            if (Components.TryGetValue(typeof(T), out var id))
            {
                return ref EasyECSMgr.GetEasyComponent<T>(id);
            }

            throw new Exception($"Can Not Find Component [{typeof(T)}]: {id}");
        }
        public bool ContainChild<T>() where T : struct, IEasyComponent
        {
            return ContainChild(typeof(T));
        }

        public bool ContainChild<T>(long id) where T : struct, IEasyComponent
        {
            return ContainChild(typeof(T), id);
        }

        public bool ContainChild(Type type)
        {
            if (Children.ContainsKey(type))
            {
                return true;
            }

            return false;
        }

        public bool ContainChild(Type type, long id)
        {
            if (Children.TryGetValue(type, out var lst))
            {
                if (lst.Contains(id))
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveEasyComponent<T>() where T : struct, IEasyComponent
        {
            RemoveEasyComponent(typeof(T));
        }

        public void RemoveEasyComponent(Type type)
        {
            if (Components.TryGetValue(type, out var id))
            {
                EasyECSMgr.EasyComponentDispose(type,id);
                Components.Remove(type);
            }
        }

        public void RemoveAllEasyComponent()
        {
            foreach (var item in Components)
            {
                EasyECSMgr.EasyComponentDispose(item.Key,item.Value);
            }
            Components.Clear();
        }


        protected void OnDestroy()
        {
            ((IEasyEntity) this).Dispose();
        }

        protected void OnEnable()
        {
            ((IEasyEntity) this).Activate();
        }

        protected void OnDisable()
        {
            ((IEasyEntity) this).Inactivate();
        }


        public virtual void OnInitialize()
        {
        }

        public virtual void OnDispose()
        {
        }

        public virtual void OnActivate()
        {
        }

        public virtual void OnInactivate()
        {
        }

        public List<long> Entitys { get; protected set; }

        public T AddEntity<T>() where T : IEasyEntity
        {
            return (T) AddEntity(typeof(T));
        }

        public IEasyEntity AddEntity(Type type)
        {
            var entity = EasyECSMgr.CreateEasyEntity(type);
            entity.Initialize();
            Entitys.Add(entity.ID);
            return entity;
        }

        public T GetEntity<T>(long id) where T : IEasyEntity
        {
            return (T) GetEntity(id);
        }

        public IEasyEntity GetEntity(long id)
        {
            return EasyECSMgr.GetEasyEntity(id);
        }
        public bool ContainEasyComponent<T>() where T : struct, IEasyComponent
        {
            return ContainEasyComponent(typeof(T));
        }

        public bool ContainEasyComponent(Type type)
        {
            if (Components.ContainsKey(type))
            {
                return true;
            }

            return false;
        }
        public void RemoveEntity(long id)
        {
            var entity = EasyECSMgr.GetEasyEntity(id);
            if (entity != null)
            {
                entity.Dispose();
                Entitys.Remove(entity.ID);
            }
        }

        public void RemoveAllEntity()
        {
            foreach (var id in Entitys)
            {
                var entity = EasyECSMgr.GetEasyEntity(id);
                entity.Dispose();
            }

            Entitys.Clear();
        }
    }

}