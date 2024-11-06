using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasySystem
{
    public enum EAccessSequence
    {
        Normal,
        Reverse
    }
    public interface IBufferContainer
    {
        int Count { get; }
        void Clear();
        void Update(float deltaTime);
    }

    public class BufferContainer<T> : IBufferContainer where T : IBufferAble
    {
        public static EAccessSequence AccessSequence;
        public static Func<T, T, bool> BufferEqualsLogic;
        public static Func<T, T, T> BufferMergeLogic;
        public static Action BufferOnAdd;
        public static float BufferLifeTime;
        public static int Count => _buffers.Count;
        public static T Current
        {
            get
            {
                return AccessSequence switch
                {
                    EAccessSequence.Normal => _buffers.FirstOrDefault(),
                    EAccessSequence.Reverse => _buffers.LastOrDefault(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        private static List<T> _buffers;
        
        int IBufferContainer.Count => Count;

        public BufferContainer()
        {
            _buffers = new List<T>();
        }

        public T this[int index] => _buffers[index];
        public T Get(int index) => _buffers[index];
        
        public void Add(T buffer)
        {
            if (BufferEqualsLogic != null)
            {
                for (int i = 0, count = _buffers.Count; i < count; i++)
                {
                    var bufferAble = _buffers[i];
                    if (BufferEqualsLogic(bufferAble, buffer))
                        if (BufferMergeLogic != null)
                            _buffers[i] = BufferMergeLogic(bufferAble, buffer);
                        else
                            return;
                }
            }

            _buffers.Add(buffer);
            BufferOnAdd?.Invoke();
        }
        public void Remove(T buffer) => _buffers.Remove(buffer);
        public void Remove()=> _buffers.Remove(Current);
        public int GetCount() => Count;
        public T GetCurrent() => Current;
        public T GetCurrent(EAccessSequence accessSequence)
        {
            return AccessSequence switch
            {
                EAccessSequence.Normal => _buffers.FirstOrDefault(),
                EAccessSequence.Reverse => _buffers.LastOrDefault(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        public T GetAndRemoveCurrent()
        {
            var t = Current;
            Remove(t);
            return t;
        }
        public T GetAndRemoveCurrent(EAccessSequence accessSequence)
        {
            var t = GetCurrent(accessSequence);
            Remove(t);
            return t;
        }
        public void Clear() => _buffers.Clear();
        public bool Contains(T buffer) => _buffers.Contains(buffer);

        void IBufferContainer.Update(float deltaTime)
        {
            for (int i = 0, count = _buffers.Count; i < count; i++)
            {
                var bufferAble = _buffers[i];
                bufferAble.ExistTime += deltaTime;
                if (bufferAble.ExistTime >= BufferLifeTime)
                    _buffers.Remove(bufferAble);
            }
        }
    }
}