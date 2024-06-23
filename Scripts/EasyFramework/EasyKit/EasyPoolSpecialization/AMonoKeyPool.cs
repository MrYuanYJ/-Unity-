using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public abstract class AMonoKeyPool<T,TValue>: MonoSingleton<T>,IInitAble where T : AMonoKeyPool<T,TValue> where TValue : Object
    {
        private readonly Dictionary<string, AMonoValuePool<TValue>> _pool = new Dictionary<string, AMonoValuePool<TValue>>();
        public void Clear()
        {
            Destroy(this);
        }
        public void ClearTarget(string key)
        {
            if (_pool.TryGetValue(key,out AMonoValuePool<TValue> pool))
            {
                pool.Clear();
                _pool.Remove(key);
            }
        }
        
        public void Recycle(string path,TValue t)
        {
            if (string.IsNullOrEmpty(path))
                path = "Unknown Path";
            if (!_pool.TryGetValue(path, out AMonoValuePool<TValue> pool))
            {
                pool = CreatePool(path);
                pool.name = path;
                _pool.Add(path, pool);
            }
            pool.Recycle(t);
        }

        public TValue Fetch(string path)
        {
            if (_pool.TryGetValue(path, out AMonoValuePool<TValue> pool))
            {
               return pool.Fetch();
            }

            return null;
        }
        protected abstract AMonoValuePool<TValue> CreatePool(string path);
    }
}