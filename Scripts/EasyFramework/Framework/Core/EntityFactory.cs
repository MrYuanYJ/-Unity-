using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class EntityFactory
    {
        private static Dictionary<Type, EasyPool<IEntity>> _pools = new();

        public static T Fetch<T>(bool init=false) where T : IEntity, new()
        {
            if (!_pools.TryGetValue(typeof(T), out EasyPool<IEntity> pool))
            {
                pool = new EasyPool<IEntity>(()=>new T(),null,null,10);
                _pools.Add(typeof(T), pool);
            }
            var entity = (T)pool.Fetch();
            if (init)
                entity.Init();
            return entity;
        }

        public static IEntity Fetch(Type type,bool init=false)
        {
            if (!_pools.TryGetValue(type, out EasyPool<IEntity> pool))
            {
                pool = new EasyPool<IEntity>(()=>(IEntity)Activator.CreateInstance(type),null,null,10);
                _pools.Add(type, pool);
            }
            var entity = pool.Fetch();
            if (init)
                entity.Init();
            return entity;
        }

        public static void Recycle<T>(T entity) where T : IEntity, new()
        {
            if (!_pools.TryGetValue(typeof(T), out EasyPool<IEntity> pool))
            {
                pool = new EasyPool<IEntity>(()=>new T(),null,null,10);
                _pools.Add(typeof(T), pool);
            }
            pool.Recycle(entity);
        }
        
        public static void Recycle(IEntity entity)
        {
            if (!_pools.TryGetValue(entity.GetType(), out EasyPool<IEntity> pool))
            {
                pool = new EasyPool<IEntity>(()=>entity,null,null,10);
                _pools.Add(entity.GetType(), pool);
            }
            pool.Recycle(entity);
        }
        
        public static void Clear()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }
            _pools.Clear();
        }
        public static void Clear<T>() where T : IEntity, new()
        {
            if (_pools.TryGetValue(typeof(T), out EasyPool<IEntity> pool))
            {
                pool.Clear();
            }
        }
    }
}