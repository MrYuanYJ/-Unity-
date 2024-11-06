using System;
using UnityEngine;

namespace EasyFramework
{
    public interface IMonoEntityCarrier
    {
        Type EntityType { get; }
        IEntity Entity { get; }
        void TryCreateEntity();
    }

    public interface IMonoEntity : IEntity
    {
        Type MonoType { get; }
        IEntity Root { get; }
    }
    public interface IMonoEntity<T> : IMonoEntity where T: AMonoEntityCarrier
    {
        Type IMonoEntity.MonoType=> typeof(T);
        T Mono { get; }
    }

    public abstract class AMonoEntity<T> : AEntity, IMonoEntity<T> where T : AMonoEntityCarrier
    {
        public T Mono => (T) BindObj;
        public IEntity Root => BindEntity;
    }
}