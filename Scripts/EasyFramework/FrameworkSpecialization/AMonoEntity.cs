using System;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public abstract class AMonoEntity<TEntity>: MonoBehaviour,IBindEntity,IEasyLife where TEntity : IEntity,new()
    {
        protected virtual void OnEnable()
        {
            this.Init();
        }

        protected virtual void OnDestroy()
        {
            if (MEntity != null && !MEntity.IsDispose)
            {
                MEntity.DisposeEvent.UnRegister(this.Dispose);
                MEntity.Dispose();
            }
        }

        IEntity IBindEntity.Entity => MEntity;
        public TEntity MEntity { get; private set; }
        public bool IsInit { get; set; }
        public EasyEvent<IInitAble> InitEvent { get; set; } = new();
        public EasyEvent<IStartAble> StartEvent { get; set; } = new();
        public EasyEvent<IDisposeAble> DisposeEvent { get; set; } = new();

        public virtual void OnInit()
        {
            MEntity = new TEntity();
            MEntity.Init();
            MEntity.RegisterOnDispose(this.Dispose);
            MEntity.EntityBind(this);
        }
        public virtual void OnStart(){}
        public virtual void OnDispose()
        {
            if(MEntity!= null&& !MEntity.IsDispose)
                MEntity.Dispose();
            GlobalEvent.RecycleGObject.InvokeEvent(gameObject);
        }
    }
}