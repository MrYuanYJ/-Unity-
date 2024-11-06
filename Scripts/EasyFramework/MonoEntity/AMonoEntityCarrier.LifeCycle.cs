
using System;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public abstract partial class AMonoEntityCarrier
    {
        protected virtual void Awake()
        {
            if (parent == null)
            {
                foreach (var child in children)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        protected virtual void OnEnable()
        {
            if (parent != null && parent.Entity == null)
                return;
            this.Init();
            if (Entity!=null && Entity.BindEntity!=null)
            {
                if (Entity.BindEntity.IsInit)
                    Entity.Init();
                else
                    Entity.BindEntity.Init();
            }
            Entity.Enable();
        }

        protected virtual void OnDisable()
        {
            if(parent!=null && parent.Entity==null)
                return;
            Entity?.Disable();
        }
        
        protected virtual void OnDestroy()
        {
            if (Entity != null && !Entity.IsDispose)
            {
                Entity.Dispose();
                Entity = null;
            }
        }
        
        void IInitAble.OnInit()
        {
            TryCreateEntity();

            Entity.EntityBind(this, Entity);
            if (parent != null)
            {
                Entity.SetParent(parent.Entity);
                parent.Entity.Container[Entity.GetType()] = Entity;
            }
            foreach (var child in children)
                child.TryInit();
        }

        void IDisposeAble.OnDispose(bool usePool)
        {
            if (Entity != null && !Entity.IsDispose)
            {
                Entity.Dispose(usePool);
                Entity = null;
            }
        }

        /// <summary>
        /// 获取或添加子级，并初始化（只建议在运行时使用此方法）
        /// </summary>
        /// <param name="monoEntityCarrierType"></param>
        public AMonoEntityCarrier GetOrAddMonoEntity(Type monoEntityCarrierType)
        {
            if (Entity.Container.TryGet(monoEntityCarrierType,out var entity))
                return entity.BindObj as AMonoEntityCarrier;
            GameObject childGo = transform.Find($"{GetType().Name}->").Out(out var child)
                ? child.gameObject
                : Instantiate(new GameObject($"{GetType().Name}->"), transform);
            if (children.Count == 0)
            {
                if (parent == null)
                    childGo.SetActive(false);
            }

            var monoEntityCarrier = (AMonoEntityCarrier)childGo.Component(monoEntityCarrierType);
            if (!children.Contains(monoEntityCarrier))
                children.Add(monoEntityCarrier);
            monoEntityCarrier.parent = this;
            monoEntityCarrier.OnEnable();
            return monoEntityCarrier;
        }
        /// <summary>
        /// 获取或添加子级，并初始化（只建议在运行时使用此方法）
        /// </summary>
        public T GetOrAddMonoEntity<T>() where T : AMonoEntityCarrier
        {
            return (T)GetOrAddMonoEntity(typeof(T));
        }

        /// <summary>
        /// 移除子级，并销毁（只建议在运行时使用此方法）
        /// </summary>
        /// <param name="monoEntityCarrierType"></param>
        public void RemoveMonoEntity(Type monoEntityCarrierType)
        {
            foreach (var child in children)
            {
                if (child.GetType()==monoEntityCarrierType)
                {
                    child.Entity.Dispose();
                    if (parent != null)
                        parent.children.Remove(this);
                    parent=null;
                    Entity = null;
                    Destroy(this);
                }
            }
        }

        /// <summary>
        /// 移除子级，并销毁（只建议在运行时使用此方法）
        /// </summary>
        public void RemoveMonoEntity<T>() where T : AMonoEntityCarrier
        {
            RemoveMonoEntity(typeof(T));
        }
    }
}