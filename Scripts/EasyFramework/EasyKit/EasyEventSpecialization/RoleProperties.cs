using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public struct RolePropertyData
    {
        public int Base;
        public int BaseAdd;
        public int BasePct;
        public int FinalAdd;
        public int FinalPct;
    }
    [Serializable]
    public class RoleProperties
    {
        public SESProperty<int> Base;
        public SESProperty<int> BaseAdd;
        public SESProperty<int> BasePct;
        public SESProperty<int> FinalAdd;
        public SESProperty<int> FinalPct;
        
        public SESProperty<float> CurrentPct;

        public int MaxValue => Mathf.FloorToInt((Base.Value * (float)(10000 + BasePct.Value)/10000 + BaseAdd.Value)*(10000 + FinalPct.Value)/10000 + FinalAdd.Value);
        [ShowInInspector]public int Value => this.GetValue();

        public RoleProperties(int baseValue,float currentPercent=1)
        {
            Base = new(baseValue);
            CurrentPct = new(currentPercent, clampLogic: Mathf.Clamp01);
            BaseAdd = new();
            BasePct = new();
            FinalAdd = new();
            FinalPct = new();
        }
        
   
        public int GetBaseValue() => Base.Value;
        public int GetBaseAdd() => BaseAdd.Value;
        public float GetBasePct() => (float)(10000 + BasePct.Value)/10000;
        public int GetFinalAdd() => FinalAdd.Value;
        public float GetFinalPct() => (float)(10000 + FinalPct.Value)/10000;
        public float GetCurrentPercent() => CurrentPct.Value;
        public float GetCurrentLostPercent() => 1 - CurrentPct.Value;
    }

    public static class RolePropertiesExt
    {
        public static int GetValue(this RoleProperties self)
        {
            if(self==null)
                return 0;
            return Mathf.FloorToInt(self.MaxValue * self.CurrentPct.Value);
        }

        public static RoleProperties AddValue(this RoleProperties self,int num)
        {
            self.CurrentPct.Value += (float) num / self.MaxValue;
            return self;
        }

        public static RoleProperties AddValuePercent(this RoleProperties self,float num)
        {
            self.CurrentPct.Value += num;
            return self;
        }

        public static RoleProperties SetValue(this RoleProperties self,int num)
        {
            self.CurrentPct.Value = (float) num / self.MaxValue;
            return self;
        }

        public static RoleProperties SetValuePercent(this RoleProperties self,float num)
        {
            self.CurrentPct.Value = num;
            return self;
        }

        public static RoleProperties AddBase(this RoleProperties self,int num,int percent)
        {
            self.BaseAdd.Value += num;
            self.BasePct.Value += percent;
            return self;
        }
        public static RoleProperties AddFinal(this RoleProperties self,int num, int percent)
        {
            self.FinalAdd.Value += num;
            self.FinalPct.Value += percent;
            return self;
        }
        public static RoleProperties SetBase(this RoleProperties self,int num, int percent)
        {
            self.Base.Value = num;
            self.BasePct.Value = percent;
            return self;
        }
        public static RoleProperties SetFinal(this RoleProperties self,int num, int percent)
        {
            self.FinalAdd.Value = num;
            self.FinalPct.Value = percent;
            return self;
        }

        public static RoleProperties AddPropertyUp(this RoleProperties self,RolePropertyData data)
        {
            self.Base.Value += data.Base;
            self.BaseAdd.Value += data.BaseAdd;
            self.BasePct.Value += data.BasePct;
            self.FinalAdd.Value += data.FinalAdd;
            self.FinalPct.Value += data.FinalPct;
            return self;
        }
        public static RoleProperties SetPropertyUp(this RoleProperties self,RolePropertyData data)
        {
            self.Base.Value = data.Base;
            self.BaseAdd.Value = data.BaseAdd;
            self.BasePct.Value = data.BasePct;
            self.FinalAdd.Value = data.FinalAdd;
            self.FinalPct.Value = data.FinalPct;
            return self;
        }
    }
    
}