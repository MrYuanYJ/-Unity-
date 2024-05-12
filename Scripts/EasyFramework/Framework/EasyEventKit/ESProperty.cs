using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    public interface IESProperty
    {
        public object GetBoxed();
        public void SetBoxed(object value);
        public void SetBoxedSilently(object value);
    }

    public interface IESProperty<T>:IESProperty
    {
        T Value { get; set; }
        T Get();
        void Set(T value);
        EasyEvent<T> PropertyEasyEvent { get; }
        Func<T, T, bool> EqualLogic { get; }
        Func<T, T> ClampLogic { get; }
        IUnRegisterHandle Register(Action<T> action);
        public void UnRegister(Action<T> action);
        void SetSilently(T value);
        void Modify<M>(M newValue, EXFunctionKit.RefFunc<T, M> refFunc);
        void Modify<M>(M newValue, EXFunctionKit.RefFunc<T, M> refFunc, Func<M, M, bool> equalLogic);
    }

    public class ESProperty<T> : IESProperty<T>
    {
        private T _value;
        private EasyEvent<T> _propertyEasyEvent;
        private Func<T, T, bool> _equalLogic;
        private Func<T, T> _clampLogic;

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
                _propertyEasyEvent.Invoke(_value);
            }
        }
        EasyEvent<T> IESProperty<T>.PropertyEasyEvent => _propertyEasyEvent;
        Func<T, T, bool> IESProperty<T>.EqualLogic => _equalLogic;
        Func<T, T> IESProperty<T>.ClampLogic => _clampLogic;
        public ESProperty()
        {
            _value = default;
            _propertyEasyEvent = new();
            _equalLogic = EqualityComparer<T>.Default.Equals;
            _clampLogic = null;
        }
        public ESProperty(T initValue, Func<T, T, bool> equalLogic = null,Func<T, T> clampLogic=null)
        {
            _value = initValue;
            _propertyEasyEvent = new();
            _equalLogic = equalLogic ?? EqualityComparer<T>.Default.Equals;
            _clampLogic = clampLogic ?? null;
        }

        public IUnRegisterHandle Register(Action<T> action)
        {
            return _propertyEasyEvent.Register(action);
        }

        public void UnRegister(Action<T> action)
        {
            _propertyEasyEvent.UnRegister(action);
        }
        public object GetBoxed() => Value;
        public void SetSilently(T value) => _value = value;
        public void SetBoxed(object value) => Value = (T) value;
        public void SetBoxedSilently(object value) => _value = (T) value;

        public void Modify<M>(M newValue,EXFunctionKit.RefFunc<T,M> refFunc)
        {
            ref var refValue = ref refFunc.Invoke(ref _value);
            if (!EqualityComparer<M>.Default.Equals(refValue,newValue))
            {
                refValue = newValue;
                _propertyEasyEvent.Invoke(_value);
            }
        }
        public void Modify<M>(M newValue,EXFunctionKit.RefFunc<T,M> refFunc,Func<M, M, bool> equalLogic)
        {
           ref var refValue = ref refFunc.Invoke(ref _value);
           equalLogic ??= EqualityComparer<M>.Default.Equals;
           if (!equalLogic(refValue,newValue))
           {
               refValue = newValue;
               _propertyEasyEvent.Invoke(_value);
           }
        }
    }

    public static class ESPropertyEx
    {
        public static void Add<T1,T2>(this IESProperty<T1> self, T2 value)  where T1: ICollection<T2>
        {
            self.Value.Add(value);
            self.PropertyEasyEvent.Invoke(self.Value);
        }

        public static void Remove<T1,T2>(this IESProperty<T1> self, T2 value)  where T1: ICollection<T2>
        {
            self.Value.Remove(value);
            self.PropertyEasyEvent.Invoke(self.Value);
        }
        public static void SetValueForceTriggerEvent<T1>(this IESProperty<T1> self, Action<T1> set)
        {
            set.Invoke(self.Value);
            self.PropertyEasyEvent.Invoke(self.Value);
        }

        public static void Clear<T>(this IESProperty<ICollection<T>> self)
        {
            if (self.Value.Count <= 0) return;
            
            self.Value.Clear();
            self.PropertyEasyEvent.Invoke(self.Value);
        }
    }
}