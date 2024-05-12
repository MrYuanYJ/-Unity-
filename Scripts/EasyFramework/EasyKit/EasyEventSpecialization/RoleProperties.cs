using UnityEngine;

namespace EasyFramework.EventKit
{
    public class RoleProperties
    {
        public readonly ESProperty<int> Base;
        public readonly ESProperty<int> BaseAdd;
        public readonly ESProperty<int> BasePct;
        public readonly ESProperty<int> FinalAdd;
        public readonly ESProperty<int> FinalPct;
        
        public readonly ESProperty<float> CurrentPct;

        public int MaxValue => Mathf.FloorToInt((Base.Value * (float)(10000 + BasePct.Value)/10000 + BaseAdd.Value)*(10000 + FinalPct.Value)/10000 + FinalAdd.Value);
        public int Value => GetValue();

        public RoleProperties(int baseValue,float currentPercent=1)
        {
            Base = new(baseValue);
            CurrentPct = new(currentPercent, clampLogic: Mathf.Clamp01);
            BaseAdd = new();
            BasePct = new();
            FinalAdd = new();
            FinalPct = new();
        }
        
        public int GetValue() => Mathf.FloorToInt(MaxValue * CurrentPct.Value);
        public int GetBaseValue() => Base.Value;
        public int GetBaseAdd() => BaseAdd.Value;
        public float GetBasePct() => (float)(10000 + BasePct.Value)/10000;
        public int GetFinalAdd() => FinalAdd.Value;
        public float GetFinalPct() => (float)(10000 + FinalPct.Value)/10000;
        public float GetCurrentPercent() => CurrentPct.Value;
        public float GetCurrentLostPercent() => 1 - CurrentPct.Value;

        public void ChangeValue(int num)=>CurrentPct.Value += (float)num / MaxValue;
        public void ChangeValuePercent(float num)=>CurrentPct.Value += num;

        public void ChangeBaseUp(int num,int percent)
        {
            BaseAdd.Value += num;
            BasePct.Value += percent;
        }
        public void ChangeFinalUp(int num, int percent)
        {
            FinalAdd.Value += num;
            FinalPct.Value += percent;
        }

    }
}