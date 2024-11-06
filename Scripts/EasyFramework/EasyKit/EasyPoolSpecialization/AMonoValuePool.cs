using System.Collections.Generic;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public abstract class AMonoValuePool<TValue> : MonoBehaviour,IPool<TValue> where TValue : Object
    {
        private readonly Stack<TValue> _pool = new();
        public int MaxCount => int.MaxValue;
        public int Count => _pool.Count;
        public string Path => gameObject.name;
        public void Clear()
        {
            Count.RepeatReverse(i =>
            {
                Destroy(_pool.Pop());
            });
        }

        public void Recycle(TValue t)
        {
            _pool.Push(t);
            t.TryGetGameObject()?.SetParent(transform).SetActive(false);
        }

        public TValue Fetch()
        {
            if (_pool.Count == 0)
                return null;
            return _pool.Pop();
        }
    }
}