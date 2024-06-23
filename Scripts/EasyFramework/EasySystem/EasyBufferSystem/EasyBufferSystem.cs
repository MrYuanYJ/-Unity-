using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasySystem
{
    public class EasyBufferSystem: ASystem
    {
        Dictionary<Type,HashSet<IBufferAble>> buffers = new();

        public void Add<T>(T buffer) where T : IBufferAble
        {
            if (!buffers.TryGetValue(typeof(T), out var bufferSet))
            {
                bufferSet = new HashSet<IBufferAble>();
                buffers.Add(typeof(T), bufferSet);
            }
            bufferSet.Add(buffer);
        }

        public T Dequeue<T>() where T : IBufferAble
        {
            if (buffers.TryGetValue(typeof(T), out var bufferSet))
            {
                var buffer = bufferSet.FirstOrDefault();
                bufferSet.Remove(buffer);
                return (T)buffer;
            }
            return default;
        }
        public T Pop<T>() where T : IBufferAble
        {
            if (buffers.TryGetValue(typeof(T), out var bufferSet))
            {
                var buffer = bufferSet.LastOrDefault();
                bufferSet.Remove(buffer);
                return (T)buffer;
            }
            return default;
        }
        
        public void Remove<T>(T buffer) where T : IBufferAble
        {
            if (buffers.TryGetValue(typeof(T), out var bufferSet))
            {
                bufferSet.Remove(buffer);
            }
        }

        public void Clear()
        {
            foreach (var bufferSet in buffers.Values)
            {
                bufferSet.Clear();
            }
        }
    }
}