using System;
using UnityEngine;

namespace EasyFramework.EasySystem
{
    public interface IPersistentData
    {
        Type DataType { get; }
        object Value { get; }
    }
    public interface IPersistentData<T>:IPersistentData
    {
        Type IPersistentData.DataType=> typeof(T);
        object IPersistentData.Value => Value;
        new T Value { get; }
    }

    [Serializable]
    public struct PersistentBool : IPersistentData<bool>
    {
        public PersistentBool(bool value)
        {
            this.value = value;
        }

        public bool Value => value;
        public bool value;
    }
    
    [Serializable]
    public struct PersistentInt : IPersistentData<int>
    {
        public PersistentInt(int value)
        {
            this.value = value;
        }

        public int Value => value;
        public int value;
    }
    
    [Serializable]
    public struct PersistentFloat : IPersistentData<float>
    {
        public PersistentFloat(float value)
        {
            this.value = value;
        }

        public float Value => value;
        public float value;
    }
    
    [Serializable]
    public struct PersistentString : IPersistentData<string>
    {
        public PersistentString(string value)
        {
            this.value = value;
        }

        public string Value => value;
        public string value;
    }
    
    [Serializable]
    public struct PersistentVector2 : IPersistentData<Vector2>
    {
        public PersistentVector2(Vector2 value)
        {
            this.x = value.x;
            this.y = value.y;
        }

        public Vector2 Value => new(x, y);
        public float x;
        public float y;
    }
    
    [Serializable]
    public struct PersistentVector3 : IPersistentData<Vector3>
    {
        public PersistentVector3(Vector3 value)
        {
            this.x = value.x;
            this.y = value.y;
            this.z = value.z;
        }

        public Vector3 Value => new(x, y, z);
        public float x;
        public float y;
        public float z;
    }

}