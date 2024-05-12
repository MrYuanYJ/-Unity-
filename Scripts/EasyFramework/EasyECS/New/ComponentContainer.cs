using System;

namespace EasyECS.New
{
    public interface IArrayContainer
    {
        long OccupyCount { get;}
        Type ValueType { get; }

        long Add<T>(T component)=>(this as IArrayContainer<T>).Add(component);

        ref T Get<T>(long uniqueId) => ref (this as IArrayContainer<T>).Get(uniqueId); 
        void DisposeComponent(long uniqueId);
    }

    public interface IArrayContainer<T> : IArrayContainer
    {
        Type IArrayContainer.ValueType => typeof(T);
        ref T[] Components { get; }
        long Add(T component);
        ref T Get(long uniqueId);
    }

    public class ArrayContainer<T> : IArrayContainer<T> where T : struct,IEasyComponent
    {
        private long _occupyCount;
        private T[] _components;
        public long OccupyCount => _occupyCount;
        public ref T[] Components => ref _components;

        public ref T this[long index]
        {
            get => ref Get(index);
        }

        public long Add(T component)
        {
            if (_components == null) _components = new T[1];
            
            if (OccupyCount >= _components.Length - 1)
            {
                Array.Resize(ref _components,_components.Length*2);
            }
            
            _components[_occupyCount] = component;
            _occupyCount++;
            return _occupyCount-1;
        }

        public ref T Get(long uniqueId)
        {
            return ref _components[uniqueId];
        }

        void IArrayContainer.DisposeComponent(long uniqueId)
        {
            EasyECSMgr.DisposeComponent(ref Get(uniqueId));
        }
    }
    
}