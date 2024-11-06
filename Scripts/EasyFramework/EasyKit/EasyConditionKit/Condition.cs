using System;
using UnityEngine;

namespace EasyFramework
{
    public enum ENormalCompare
    {
        Equals,
        NotEquals,
    }
    public enum ENumCompare
    {
        Equals,
        NotEquals,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
    }

    public enum EStringCompare
    {
        Equals,
        NotEquals,
        Contains,
        StartWith,
        EndWith,
    }
    [Serializable]
    public struct BoolCondition: ICondition<bool>
    {
        public ENormalCompare Condition;
        public bool Value;
        public bool IsTure(bool input)
        {
            return Condition switch
            {
                ENormalCompare.Equals => input == Value,
                ENormalCompare.NotEquals => input != Value,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    [Serializable]
    public struct IntCondition: ICondition<int>
    {
        public ENumCompare Condition;
        public int Value;
        public bool IsTure(int input)
        {
            return Condition switch
            {
                ENumCompare.Equals => input == Value,
                ENumCompare.NotEquals => input != Value,
                ENumCompare.GreaterThan => input > Value,
                ENumCompare.GreaterThanOrEqual => input >= Value,
                ENumCompare.LessThan => input < Value,
                ENumCompare.LessThanOrEqual => input <= Value,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    [Serializable]
    public struct FloatCondition: ICondition<float>
    {
        public ENumCompare Condition;
        public float Value;
        public bool IsTure(float input)
        {
            return Condition switch
            {
                ENumCompare.Equals => input == Value,
                ENumCompare.NotEquals => input != Value,
                ENumCompare.GreaterThan => input > Value,
                ENumCompare.GreaterThanOrEqual => input >= Value,
                ENumCompare.LessThan => input < Value,
                ENumCompare.LessThanOrEqual => input <= Value,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    [Serializable]
    public struct StringCondition: ICondition<string>
    {
        public EStringCompare Condition;
        public string Value;
        public bool IsTure(string input)
        {
            return Condition switch
            {
                EStringCompare.Equals => input == Value,
                EStringCompare.NotEquals => input != Value,
                EStringCompare.Contains => input.Contains(Value),
                EStringCompare.StartWith => input.StartsWith(Value),
                EStringCompare.EndWith => input.EndsWith(Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    [Serializable]
    public struct Vector2Condition : ICondition<Vector2>
    {
        public ENumCompare Condition;
        public Vector2 Value;
        public bool IsTure(Vector2 input)
        {
            return Condition switch
            {
                ENumCompare.Equals => input == Value,
                ENumCompare.NotEquals => input != Value,
                ENumCompare.GreaterThan => input.x > Value.x && input.y > Value.y,
                ENumCompare.GreaterThanOrEqual => input.x >= Value.x && input.y >= Value.y,
                ENumCompare.LessThan => input.x < Value.x && input.y < Value.y,
                ENumCompare.LessThanOrEqual => input.x <= Value.x && input.y <= Value.y,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    [Serializable]
    public struct Vector3Condition : ICondition<Vector3>
    {
        public ENumCompare Condition;
        public Vector3 Value;

        public bool IsTure(Vector3 input)
        {
            return Condition switch
            {
                ENumCompare.Equals => input == Value,
                ENumCompare.NotEquals => input != Value,
                ENumCompare.GreaterThan => input.x > Value.x && input.y > Value.y && input.z > Value.z,
                ENumCompare.GreaterThanOrEqual => input.x >= Value.x && input.y >= Value.y && input.z >= Value.z,
                ENumCompare.LessThan => input.x < Value.x && input.y < Value.y && input.z < Value.z,
                ENumCompare.LessThanOrEqual => input.x <= Value.x && input.y <= Value.y && input.z <= Value.z,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}