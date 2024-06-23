using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyFramework
{
    public class ContainerDic<TResult>: IEnumerable<KeyValuePair<Type, TResult>>
    {
        private Dictionary<Type, TResult> Dic { get;} = new();
        public int Count => Dic.Count;
        public Dictionary<Type, TResult>.KeyCollection Keys => Dic.Keys;
        public Dictionary<Type, TResult>.ValueCollection Values => Dic.Values;

        public void Clear()
        {
            Dic.Clear();
        }
        public void Set<T>(T obj) where T : TResult => Dic[typeof(T)] = obj;
        public void Set(Type type, TResult obj) => Dic[type] = obj;
        public void Add<T>(T obj) where T : TResult
        {
            if (obj != null)
                Dic.Add(typeof(T), obj);
        }

        public void Add(Type type, TResult obj)
        {
            if (obj != null)
                Dic.Add(type, obj);
        }

        public bool TryAdd<T>(T obj) where T : TResult => Dic.TryAdd(typeof(T), obj);
        public bool TryAdd(Type type, TResult obj) => Dic.TryAdd(type, obj);

        public T Get<T>() where T : TResult => (T) Dic[typeof(T)];
        public TResult Get(Type type)=> Dic[type];

        public bool TryGet<T>(out T obj) where T : TResult
        {
            if (Dic.TryGetValue(typeof(T), out var value))
            {
                obj = (T) value;
                return true;
            }

            obj = default;
            return false;
        }
        public bool TryGet(Type type, out TResult obj)
        {
            if (Dic.TryGetValue(type, out var value))
            {
                obj = value;
                return true;
            }

            obj = default;
            return false;
        }

        public bool Remove<T>() where T : TResult => Dic.Remove(typeof(T));
        public bool Remove(Type type)=> Dic.Remove(type);

        public bool Has<T>() where T : TResult => Dic.ContainsKey(typeof(T));
        public bool Has(Type type)=> Dic.ContainsKey(type);
        
        public IEnumerator<KeyValuePair<Type, TResult>> GetEnumerator()
        {
            return Dic.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class ContainerDicExtensions
    {
        public static void InitAll<T>(this ContainerDic<T> container) where T : IEasyLife
        {
            foreach (var obj in container.Values)
                obj.Init();
        }

        public static void TryInitAll(this ContainerDic<object> container)
        {
            foreach (var obj in container.Values)
                obj.TryInit();
        }
        public static void DisposeAll<T>(this ContainerDic<T> container) where T : IEasyLife
        {
            foreach (var obj in container.Values)
                obj.Dispose();
        }

        public static void TryDisposeAll(this ContainerDic<object> container)
        {
            foreach (var obj in container.Values)
                obj.TryDispose();
        }
    }
}