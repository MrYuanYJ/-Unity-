using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IPool
    {
        int MaxCount { get; }
        int Count { get; }
        void Recycle<T>(T t);
        T Fetch<T>();
        void Clear();
    }

    public interface IPool<T>: IPool
    {
        void IPool.Recycle<TValue>(TValue value)
        {
            if(value is T t)
                Recycle(t);
        }
        TValue IPool.Fetch<TValue>()
        {
            if (Fetch() is TValue value)
                return value;
            throw new Exception($"[{typeof(T).Name}] can not change to [{typeof(TValue).Name}]");
        }
        void Recycle(T t);
        T Fetch();
    }
    public interface IRecycleable
    {
        bool IsRecycled { get; set; }
        void Recycle();
    }

    public abstract class APool<T> : IPool<T> where T : class
    {
        protected readonly Stack<T> pool = new Stack<T>();
        protected Func<T> Creator;
        protected Action<T> OnRecycle;
        protected Action<T> OnFetch;
        protected int _maxCount=16;

        public int MaxCount => _maxCount;
        public int Count=>pool.Count;
        public void Clear()=>pool.Clear();
        public void SetCreator(Func<T> creator)=>Creator = creator;
        public void SetOnRecycle(Action<T> onRecycle)=>OnRecycle = onRecycle;
        public void SetOnFetch(Action<T> onFetch) => OnFetch = onFetch;
        public virtual T Fetch()
        {
            T t= pool.Count==0?
                Creator():
                pool.Pop();
            if (t is IRecycleable recycleable)
            {
                recycleable.IsRecycled = false;
            }

            OnFetch?.Invoke(t);
            return t;
        }


        public virtual void Recycle(T t)
        {
            if ( t == null ) return;
            if(t is IRecycleable recycleable)
            {
                if (recycleable.IsRecycled) return;
                recycleable.IsRecycled = true;
                recycleable.Recycle();
            }

            if ( pool.Count <= MaxCount )
            {
                OnRecycle?.Invoke( t );
                pool.Push( t );
            }
        }
    }

    public class EasyPool<T> : APool<T> where T : class
    {
        public EasyPool(Func<T> creator, Action<T> onRecycle, Action<T> onFetch, int maxCount = 16)
        {
            SetCreator(creator);
            SetOnRecycle(onRecycle);
            SetOnFetch(onFetch);
            _maxCount = maxCount;
        }
    }
}
