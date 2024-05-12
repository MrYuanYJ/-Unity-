using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyECS.New
{
    public interface IDispose : IDisposable
    {
        bool IsDispose { get; protected set; }
    }

    public interface IActivatable
    {
        bool IsActive{ get; protected set; }

        void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                if (this is MonoBehaviour mono && !mono.gameObject.activeSelf)
                {
                    mono.gameObject.SetActive(true);
                }
                OnActivate();
            }
        }

        void Inactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                if (this is MonoBehaviour mono && mono.gameObject.activeSelf)
                {
                    mono.gameObject.SetActive(false);
                }
                OnInactivate();
            }
        }

        void OnActivate();
        void OnInactivate();
    }

    public interface IUnique
    {
        long ID { get; set; }
    }

    public interface IEasyEntityContainer
    {
        List<long> Entitys { get;}

        T AddEntity<T>() where T : IEasyEntity;
        IEasyEntity AddEntity(Type type);
        T GetEntity<T>(long id) where T : IEasyEntity;
        IEasyEntity GetEntity(long id);
        void RemoveEntity(long id);
        void RemoveAllEntity();
    }
    public interface IEasyComponentContainer
    { 
        Dictionary<Type, long> Components { get;}

        ref T AddEasyComponent<T>() where T : struct,IEasyComponent;
        ref T GetEasyComponent<T>() where T : struct,IEasyComponent;
        bool ContainEasyComponent<T>() where T : struct,IEasyComponent;
        bool ContainEasyComponent(Type type);
        void RemoveEasyComponent<T>() where T : struct,IEasyComponent;
        void RemoveEasyComponent(Type type);
        void RemoveAllEasyComponent();
    }

    public interface IChildEasyComponentContainer
    {
        Dictionary<Type,List<long>> Children { get; }

        ref T AddChild<T>() where T : struct,IEasyComponent;
        ref T GetChild<T>(long id) where T : struct,IEasyComponent;
        bool ContainChild<T>() where T : struct,IEasyComponent;
        bool ContainChild<T>(long id) where T : struct,IEasyComponent;
        bool ContainChild(Type type);
        bool ContainChild(Type type, long id);
        void RemoveChild<T>(long id) where T : struct, IEasyComponent;
        void RemoveChild(Type type, long id);
        void RemoveAllChild();
    }

    public interface IEasyComponent
    {
        long ID { get; set; }
        IEasyEntity Entity { get;  set; }
        bool IsDispose { get; set; }
    }

    public interface IEasyEntity :
        IEasyEntityContainer,
        IEasyComponentContainer,
        IChildEasyComponentContainer,
        IUnique,
        IActivatable,
        IDispose
    {
        public void Initialize()
        {
            IsDispose = false;
            OnInitialize();
        }

        void OnInitialize();
        
        void IDisposable.Dispose()
        {
            if ( IsDispose ) return;

            IsDispose = true;
            OnDispose();
            RemoveAllEasyComponent();
            RemoveAllChild();

            if (this is MonoBehaviour mono)
            {
                GameObject.Destroy(mono);
            }
            EasyECSMgr.EasyEntityDispose(this);
        }

        void OnDispose();
        
        void IActivatable.Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                if (this is MonoBehaviour mono && !mono.gameObject.activeSelf)
                {
                    mono.gameObject.SetActive(true);
                }
                OnActivate();
            }
        }

        void IActivatable.Inactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                if (this is MonoBehaviour mono && mono.gameObject.activeSelf)
                {
                    mono.gameObject.SetActive(false);
                }
                OnInactivate();
            }
        }
    }

    public interface IEasyComponentSystem: IEasyAwake,IEasyDestroy
    {
        public Type ComponentType { get; }
        public IArrayContainer Components { get; }

        public IEasyComponentSystem Init()
        {
            if(IsAwake) return this;
            
            IsAwake = true;
            IsDestroy = false;

            Awake();
            if (this is IEasyUpdate update)
            {
                EasyECSMgr.OnUpdate += update.Update;
            }
            if (this is IEasyFixedUpdate fixedUpdate)
            {
                EasyECSMgr.OnFixedUpdate += fixedUpdate.FixedUpdate;
            }

            return this;
        }
        void IEasyDestroy.Destroy()
        {
            if (IsDestroy) return;

            IsDestroy = true;
            IsAwake = false;
            if (this is IEasyUpdate update)
            {
                EasyECSMgr.OnUpdate -= update.Update;
            }

            if (this is IEasyFixedUpdate fixedUpdate)
            {
                EasyECSMgr.OnFixedUpdate -= fixedUpdate.FixedUpdate;
            }
        }
    }
    public interface IEasyAwake
    { 
        bool IsAwake { get; set; }
        Action onAwake { get; set; }
        void Awake()
        {
            OnAwake();
            onAwake?.Invoke();
        }

        void OnAwake();
    }
    public interface IEasyUpdate
    {
        Action onUpdate { get; set; }

        void Update()
        {
            OnUpdate();
            onUpdate?.Invoke();
        }
        void OnUpdate();
    }
    public interface IEasyFixedUpdate
    {
        Action onFixedUpdate { get; set; }

        void FixedUpdate()
        {
            OnFixedUpdate();
            onFixedUpdate?.Invoke();
        }
        void OnFixedUpdate();
    }
    public interface IEasyDestroy
    {
        bool IsDestroy { get; set; }
        Action onDestroy { get; set; }

        void Destroy()
        {
            OnDestroy();
            onDestroy?.Invoke();
        }
        void OnDestroy();
    }
    public interface IEasyComponentSystem<T> : IEasyComponentSystem where T: struct,IEasyComponent
    {
        Type IEasyComponentSystem.ComponentType => typeof(T);
        IArrayContainer IEasyComponentSystem.Components => Container;
        static IArrayContainer<T> Container => (IArrayContainer<T>) EasyECSMgr.AllEasyComponentDic[typeof(T)];

        void OnComponentAwake(ref T component);
        void OnComponentDestroy(ref T component);
    }
    
}