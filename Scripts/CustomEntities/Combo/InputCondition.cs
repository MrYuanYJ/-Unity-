using System;

namespace EasyFramework
{
    [Serializable]
    public struct InputCondition: ICondition
    {
        public EInput Input;
        public FloatConditionGroup PressedTimeCondition; 
        public bool IsTure()
        {
           return PressedTimeCondition.IsTure(EasyInputSetting.Instance.InputStateDict[Input].TimePressed.Value);
        }
    }
}