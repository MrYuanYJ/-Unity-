using System;
using UnityEngine;

namespace EasyFramework
{
    public partial interface IBBValue
    {
        object Value { get; internal set; }
        Type ValueType { get; }
    }
    [System.Serializable]
    public abstract partial class BBValue<T> : ScriptableObject,IBBValue
    {
        public T value;
     
        object IBBValue.Value
        {
            get => value;
            set => this.value = (T)value;
        }

        public virtual Type ValueType => typeof(T);
    }

    [System.Serializable]
    public struct BBValueView
    {
        public string key;
        public ScriptableObject serializedValue;
    }
}