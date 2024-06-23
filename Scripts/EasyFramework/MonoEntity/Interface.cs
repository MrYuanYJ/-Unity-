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

    internal interface IMonoEntity : IEntity
    {
        Type MonoType { get; }
    }
    internal interface IMonoEntity<T> : IMonoEntity where T: MonoBehaviour
    {
        Type IMonoEntity.MonoType=> typeof(T);
        T Mono { get; }
        IEntity Root { get; }
    }

    public abstract class AMonoEntity<T> : AEntity, IMonoEntity<T> where T : MonoBehaviour
    {
        public T Mono => (T) BindObj;
        public IEntity Root => BindEntity;
    }
}