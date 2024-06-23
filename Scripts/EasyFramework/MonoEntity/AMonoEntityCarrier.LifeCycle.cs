
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
            if (Entity is {BindEntity: not null})
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
                parent.Entity.Container.Add(Entity.GetType(), Entity);
            }
            foreach (var child in children)
                child.TryInit();
        }

        void IDisposeAble.OnDispose(bool usePool)
        {
            if (Entity != null && !Entity.IsDispose)
            {
                Entity.Dispose();
                Entity = null;
            }
        }

        /// <summary>
        /// 添加子级，并初始化（只建议在运行时使用此方法）
        /// </summary>
        /// <param name="monoEntityCarrierType"></param>
        public AMonoEntityCarrier AddMonoEntity(Type monoEntityCarrierType)
        {
            if (Entity.Container.TryGet(monoEntityCarrierType,out var entity))
                return entity.BindObj as AMonoEntityCarrier;
            GameObject childGo = transform.Find($"{GetType().Name}->").Out(out var child)
                ? child.gameObject
                : Instantiate(new GameObject($"{GetType().Name}->"), transform);
            if (children.Count == 0)
            {
                childGo.transform.SetParent(transform);
                if (parent == null)
                    childGo.SetActive(false);
            }

            var monoEntityCarrier = (AMonoEntityCarrier)childGo.AddComponent(monoEntityCarrierType);
            children.Add(monoEntityCarrier);
            monoEntityCarrier.parent = this;
            monoEntityCarrier.OnEnable();
            return monoEntityCarrier;
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
    }
}