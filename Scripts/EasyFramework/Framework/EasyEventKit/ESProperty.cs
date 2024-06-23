using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXFunctionKit;

namespace EasyFramework.EventKit
{
    public interface IESProperty: IDisposeAble
    {
        object GetBoxed();
        void SetBoxed(object value);
        void SetBoxedSilently(object value);
    }

    public interface IESProperty<T>:IESProperty
    {
        T Value { get; set; }
        T Get();
        void Set(T value);
        EasyEvent<T> PropertyEvent { get; }
        Func<T, T, bool> EqualLogic { get; }
        Func<T, T> ClampLogic { get; }
        IUnRegisterHandle Register(Action action);
        IUnRegisterHandle Register(Action<T> action);
        void UnRegister(Action action);
        public void UnRegister(Action<T> action);
        void SetSilently(T value);
        void Modify<M>(M newValue, RefFunc<T, M> refFunc);
        void Modify<M>(M newValue, RefFunc<T, M> refFunc, Func<M, M, bool> equalLogic);
        void ForceNotify()
        {
            PropertyEvent.Invoke(Value);
        }
    }
    public class ESProperty<T> : IESProperty<T>
    {
        private T _value;
        private readonly EasyEvent<T> _propertyEvent;
        private readonly Func<T, T, bool> _equalLogic;
        private readonly Func<T, T> _clampLogic;

        public T Value
        {
            get => Get();
            set => Set(value);
        }

        public T Get() => _value;

        public void Set(T value)
        {
            if (_clampLogic != null)
                value = _clampLogic.Invoke(value);
            if (!_equalLogic(_value, value))
            {
                _value = value;
                _propertyEvent.Invoke(_value);
            }
        }
        EasyEvent<T> IESProperty<T>.PropertyEvent => _propertyEvent;
        Func<T, T, bool> IESProperty<T>.EqualLogic => _equalLogic;
        Func<T, T> IESProperty<T>.ClampLogic => _clampLogic;
        public ESProperty()
        {
            _value = default;
            _propertyEvent = new();
            _equalLogic = EqualityComparer<T>.Default.Equals;
            _clampLogic = null;
        }
        public ESProperty(T initValue, Func<T, T, bool> equalLogic = null,Func<T, T> clampLogic=null)
        {
            _value = initValue;
            _propertyEvent = new();
            _equalLogic = equalLogic ?? EqualityComparer<T>.Default.Equals;
            _clampLogic = clampLogic;
        }
        
        public IUnRegisterHandle Register(Action action)=>_propertyEvent.Register(action);
        public IUnRegisterHandle Register(Action<T> action)=>_propertyEvent.Register(action);
        public IUnRegisterHandle InvokeAndRegister(Action action)
        {
            action.Invoke();
            return _propertyEvent.Register(action);
        }
        public IUnRegisterHandle InvokeAndRegister(Action<T> action)
        {
            action.Invoke(_value);
            return _propertyEvent.Register(action);
        }

        public void UnRegister(Action action)=>_propertyEvent.UnRegister(action);
        public void UnRegister(Action<T> action)=>_propertyEvent.UnRegister(action);
        public void Clear() => _propertyEvent.Clear();
        
        public object GetBoxed() => Value;
        public void SetSilently(T value) => _value = value;
        public void SetBoxed(object value) => Value = (T) value;
        public void SetBoxedSilently(object value) => _value = (T) value;

        public void Modify<M>(M newValue,RefFunc<T,M> refFunc)
        {
            ref var refValue = ref refFunc.Invoke(ref _value);
            if (!EqualityComparer<M>.Default.Equals(refValue,newValue))
            {
                refValue = newValue;
                _propertyEvent.Invoke(_value);
            }
        }
        public void Modify<M>(M newValue,RefFunc<T,M> refFunc,Func<M, M, bool> equalLogic)
        {
           ref var refValue = ref refFunc.Invoke(ref _value);
           equalLogic ??= EqualityComparer<M>.Default.Equals;
           if (!equalLogic(refValue,newValue))
           {
               refValue = newValue;
               _propertyEvent.Invoke(_value);
           }
        }
        public void Modify<M>(Expression<Func<T, M>> expression, M newValue)
        {
            ExCSharp.Modify(Value, expression, newValue, _propertyEvent.Invoke);
        }

        public bool IsDispose{ get; set; }
        public IEasyEvent DisposeEvent { get; } = new EasyEvent();

        void IDisposeAble.OnDispose(bool usePool)
        {
            _value = default;
            _propertyEvent.Clear();
        }
    }

    public static class ESPropertyEx
    {
        public static void Add<T>(this IESProperty<List<T>> self, T value)
        {
            self.Value.Add(value);
            self.PropertyEvent.Invoke(self.Value);
        }
        public static bool Add<K, V>(this IESProperty<Dictionary<K, V>> self, K key, V value)
        {
            if(self.Value.TryAdd(key, value))
            {
                self.PropertyEvent.Invoke(self.Value);
                return true;
            }
            return false;
        }
        public static bool Add<T>(this IESProperty<HashSet<T>> self, T value)
        {
            if(self.Value.Add(value))
            {
                self.PropertyEvent.Invoke(self.Value);
                return true;
            }
            return false;
        }
        public static void Enqueue<T>(this IESProperty<Queue<T>> self, T value)
        {
            self.Value.Enqueue(value);
            self.PropertyEvent.Invoke(self.Value);
        }
        public static void Push<T>(this IESProperty<Stack<T>> self, T value)
        {
            self.Value.Push(value);
            self.PropertyEvent.Invoke(self.Value);
        }
        
        
        public static bool Remove<T>(this IESProperty<List<T>> self, T value)
        {
            if (!self.Value.Remove(value)) return false;
            self.PropertyEvent.Invoke(self.Value);
            return true;

        }
        public static bool Remove<K, V>(this IESProperty<Dictionary<K, V>> self, K key)
        {
            if (!self.Value.Remove(key)) return false;
            self.PropertyEvent.Invoke(self.Value);
            return true;
        }
        public static bool Remove<T>(this IESProperty<HashSet<T>> self, T value)
        {
            if (!self.Value.Remove(value)) return false;
            self.PropertyEvent.Invoke(self.Value);
            return true;
        }
        public static void Dequeue<T>(this IESProperty<Queue<T>> self)
        {
            self.Value.Dequeue();
            self.PropertyEvent.Invoke(self.Value);
        }
        public static T Pop<T>(this IESProperty<Stack<T>> self)
        {
            var value =self.Value.Pop();
            self.PropertyEvent.Invoke(self.Value);
            return value;
        }
        
        
        public static void SetValueForceTriggerEvent<T>(this IESProperty<T> self, Action<T> set)
        {
            set.Invoke(self.Value);
            self.PropertyEvent.Invoke(self.Value);
        }

        public static void Clear<T>(this IESProperty<List<T>> self)
        {
            if (self.Value.Count <= 0) return;
            
            self.Value.Clear();
            self.PropertyEvent.Invoke(self.Value);
        }
        public static void Clear<K, V>(this IESProperty<Dictionary<K, V>> self)
        {
            if (self.Value.Count <= 0) return;
            
            self.Value.Clear();
            self.PropertyEvent.Invoke(self.Value);
        }
        public static void Clear<T>(this IESProperty<HashSet<T>> self)
        {
            if (self.Value.Count <= 0) return;
            
            self.Value.Clear();
            self.PropertyEvent.Invoke(self.Value);
        }
        public static void Clear<T>(this IESProperty<Queue<T>> self)
        {
            if (self.Value.Count <= 0) return;
            
            self.Value.Clear();
            self.PropertyEvent.Invoke(self.Value);
        }

        public static void Clear<T>(this IESProperty<Stack<T>> self)
        {
            if (self.Value.Count <= 0) return;

            self.Value.Clear();
            self.PropertyEvent.Invoke(self.Value);
        }
    }
}