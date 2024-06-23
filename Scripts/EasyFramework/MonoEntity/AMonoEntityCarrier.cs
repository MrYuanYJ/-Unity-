using System;
using System.Collections.Generic;
using EasyFramework.EasySystem.EasyAttributeSystem;
using EasyFramework.EventKit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    public abstract partial class AMonoEntityCarrier : MonoBehaviour,IMonoEntityCarrier,IEasyLife
    {
        public Type EntityType { get; private set; }
        public IEntity Entity { get; private set; }

        public virtual void TryCreateEntity()
        {
            if (Entity != null && Entity.GetType() == EntityType)
                return;
            
            EntityType ??= Game.TryRegister().System<EasyMono2EntityRelationalMappingSystem>().Mono2EntityMapping[GetType()];
            Entity = (IEntity)ReferencePool.Fetch(EntityType, false, false);
        }
        
        [SerializeField][ShowIf("@childrenTrans!=null")] protected List<AMonoEntityCarrier> children = new();
        [SerializeField][ShowIf("@parent!=null")][ReadOnly] protected AMonoEntityCarrier parent;
        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; set; } = new EasyEvent();
        public IEasyEvent DisposeEvent { get; set; }= new EasyEvent();
    }
}