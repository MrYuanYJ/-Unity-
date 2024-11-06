using System;
using System.Collections.Generic;

namespace EasyFramework.EasySystem
{
    public class EasyBufferSystem: ASystem,IUpdateAble
    {
        Dictionary<Type,IBufferContainer> buffers = new();

        public static void SetAccessSequence<T>(EAccessSequence accessSequence) where T : IBufferAble => BufferContainer<T>.AccessSequence = accessSequence;
        public static void SetEquals<T>(Func<T,T,bool> equals) where T : IBufferAble =>BufferContainer<T>.BufferEqualsLogic = equals;
        public static void SetMerge<T>(Func<T,T,T> merge) where T : IBufferAble =>BufferContainer<T>.BufferMergeLogic = merge;
        public static void SetOnAdd<T>(Action onAdd) where T : IBufferAble =>BufferContainer<T>.BufferOnAdd = onAdd;
        
        public BufferContainer<T> Container<T>() where T : IBufferAble
        {
            if (!buffers.TryGetValue(typeof(T), out var bufferContainer))
            {
                bufferContainer = new BufferContainer<T>();
                buffers.Add(typeof(T), bufferContainer);
            }
            return (BufferContainer<T>) bufferContainer;
        }

        public void Add<T>(T buffer) where T : IBufferAble
        {
            Container<T>().Add(buffer);
        }
        public T GetCurrent<T>() where T : IBufferAble
        {
            return Container<T>().GetCurrent();
        }
        public T GetCurrent<T>(EAccessSequence accessSequence) where T : IBufferAble
        {
            return Container<T>().GetCurrent(accessSequence);
        }
        public T GetAndRemoveCurrent<T>() where T : IBufferAble
        {
            return Container<T>().GetAndRemoveCurrent();
        }
        public T GetAndRemoveCurrent<T>(EAccessSequence accessSequence) where T : IBufferAble
        {
            return Container<T>().GetAndRemoveCurrent(accessSequence);
        }
        public void Remove<T>(T buffer) where T : IBufferAble
        {
            Container<T>().Remove(buffer);
        }
        public void Remove<T>() where T : IBufferAble
        {
            Container<T>().Remove();
        }

        public void Clear<T>() where T : IBufferAble
        {
            Container<T>().Clear();
        }
        public void Clear()
        {
            foreach (var bufferContainer in buffers.Values)
            {
                bufferContainer.Clear();
            }
        }

        public IEasyEvent UpdateEvent { get; } = new EasyEvent();
        void IUpdateAble.OnUpdate()
        {
            foreach (var bufferContainer in buffers.Values)
            {
                bufferContainer.Update(EasyTime.DeltaTime);
            }
        }
    }
}