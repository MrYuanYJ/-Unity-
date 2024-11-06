using System;
using UnityEngine;

namespace EasyFramework
{
    [Serializable]
    public struct BoolConditionGroup: IConditionGroup<bool>
    {
        public EConditionType ConditionType;
        public BoolCondition[] Conditions;
        public bool IsTure(bool input)
        {
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(bool input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(bool input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
    [Serializable]
    public struct IntConditionGroup: ICondition<int>
    {
        public EConditionType ConditionType;
        public IntCondition[] Conditions;

        public bool IsTure(int input)
        {
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(int input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(int input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
    [Serializable]
    public struct FloatConditionGroup: ICondition<float>
    {
        public EConditionType ConditionType;
        public FloatCondition[] Conditions;

        public bool IsTure(float input)
        {
            if(Conditions.Length == 0)
                return true;
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(float input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(float input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
    [Serializable]
    public struct StringConditionGroup: ICondition<string>
    {
        public EConditionType ConditionType;
        public StringCondition[] Conditions;

        public bool IsTure(string input)
        {
            if(Conditions.Length == 0)
                return true;
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(string input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(string input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
    
    [Serializable]
    public struct Vector2ConditionGroup: ICondition<Vector2>
    {
        public EConditionType ConditionType;
        public Vector2Condition[] Conditions;

        public bool IsTure(Vector2 input)
        {
            if(Conditions.Length == 0)
                return true;
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(Vector2 input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(Vector2 input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
    [Serializable]
    public struct Vector3ConditionGroup: ICondition<Vector3>
    {
        public EConditionType ConditionType;
        public Vector3Condition[] Conditions;
        
        public bool IsTure(Vector3 input)
        {
            if(Conditions.Length == 0)
                return true;
            return ConditionType switch
            {
                EConditionType.And => And(input),
                EConditionType.Or => Or(input),
                _ => false
            };
        }
        private bool And(Vector3 input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsTure(input))
                    return false;
            }
            return true;
        }
        private bool Or(Vector3 input)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].IsTure(input))
                    return true;
            }

            return false;
        }
    }
}