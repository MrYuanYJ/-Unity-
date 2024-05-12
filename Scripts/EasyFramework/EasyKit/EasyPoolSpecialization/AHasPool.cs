using System;

namespace EasyFramework
{
    public abstract class AHasPool<T>: AutoSingleton<AHasPool<T>> where T : class, IRecycleable
    {
        protected abstract Func<T> Creator { get; }
        protected abstract Action<T> OnRecycle { get; }
        protected abstract Action<T> OnFetch { get; }
        protected abstract int MaxCount { get; }
        
        private static Func<T> creator => Instance.Creator;
        private static Action<T> onRecycle => Instance.OnRecycle;
        private static Action<T> onFetch => Instance.OnFetch;
        private static int maxCount => Instance.MaxCount;
        
        private static EasyPool<T> _pool = new EasyPool<T>(creator,onRecycle,onFetch,maxCount);

        public static T Fetch() => _pool.Fetch();
        public static void Recycle(T t) => _pool.Recycle(t);
        public static void Clear() => _pool.Clear();
        public static int Count() => _pool.Count;
    }
}