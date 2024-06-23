using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class ReferencePool
    {
        private static Dictionary<Type, EasyPool<object>> _pools = new();

        public static T Fetch<T>() where T : class, new()=>Fetch<T>(false);
        
        public static T Fetch<T>(bool init) where T : class, new() => Fetch<T>(init,true);
        
        public static T Fetch<T>(bool init,bool usePool) where T : class, new()
        {
            if(!usePool)
                return new T();
            if (!_pools.TryGetValue(typeof(T), out EasyPool<object> pool))
            {
                pool = new EasyPool<object>(()=>new T(),null,null,10);
                _pools.Add(typeof(T), pool);
            }
            var obj =(T) pool.Fetch();
            if (init)
                obj.TryInit();
            return obj;
        }

        public static object Fetch(Type type)=>Fetch(type,true);
        public static object Fetch(Type type,bool init)=> Fetch(type,false,init);
        public static object Fetch(Type type,bool init,bool usePool)
        {
            if(!usePool)
                return Activator.CreateInstance(type);
            if (!_pools.TryGetValue(type, out EasyPool<object> pool))
            {
                pool = new EasyPool<object>(()=>Activator.CreateInstance(type),null,null,10);
                _pools.Add(type, pool);
            }
            var obj = pool.Fetch();
            if (init)
                obj.TryInit();
            return obj;
        }

        public static void Recycle<T>(T obj) where T : class, new()
        {
            if (!_pools.TryGetValue(typeof(T), out EasyPool<object> pool))
            {
                pool = new EasyPool<object>(()=>new T(),null,null,10);
                _pools.Add(typeof(T), pool);
            }
            pool.Recycle(obj);
        }
        
        public static void Recycle(object obj)
        {
            if (!_pools.TryGetValue(obj.GetType(), out EasyPool<object> pool))
            {
                pool = new EasyPool<object>(()=>obj,null,null,10);
                _pools.Add(obj.GetType(), pool);
            }
            pool.Recycle(obj);
        }
        
        public static void Clear()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }
            _pools.Clear();
        }
        public static void Clear<T>() where T : class, new()
        {
            if (_pools.TryGetValue(typeof(T), out EasyPool<object> pool))
            {
                pool.Clear();
            }
        }
    }
}