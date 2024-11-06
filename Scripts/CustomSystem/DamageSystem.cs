using System;
using System.Collections.Generic;
using EasyFramework.EasySystem;
using UnityEngine;

namespace EasyFramework
{
    public struct DamageRelatedInfo
    {
        private static Dictionary<EDamage, DamageRelatedInfo> _damageRelatedInfoDict = new();
        public string gain;
        public string defense;
        public string shield;
        public string guaranteedDamage;

        static DamageRelatedInfo()
        {
            foreach (EDamage damageType in Enum.GetValues(typeof(EDamage)))
            {
                string damageTypeStr = damageType.ToString();
                _damageRelatedInfoDict[damageType] = new DamageRelatedInfo()
                {
                    gain = $"{damageTypeStr}Gain",
                    defense = $"{damageTypeStr}Defense",
                    shield = $"{damageTypeStr}Shield",
                    guaranteedDamage = $"{damageTypeStr}GuaranteedDamage"
                };
            }
        }

        public static string Gain(EDamage damageType)=> _damageRelatedInfoDict[damageType].gain;
        public static string Defense(EDamage damageType) => _damageRelatedInfoDict[damageType].defense;
        public static string Shield(EDamage damageType) => _damageRelatedInfoDict[damageType].shield;
        public static string GuaranteedDamage(EDamage damageType) => _damageRelatedInfoDict[damageType].guaranteedDamage;
    }
    public class DamageSystem: ASystem
    {
        private BufferContainer<DamageInfo> _damageBuffer;
        private DamageSettlementProcedureMachine _machine;
        private bool _pollingInThisFrame;
        protected override void OnInit()
        {
            EasyBufferSystem.SetEquals<DamageInfo>(DamageInfoEquals);
            EasyBufferSystem.SetMerge<DamageInfo>(DamageInfoMerge);
            EasyBufferSystem.SetOnAdd<DamageInfo>(PollingInThisFrameEnd);
            this.RegisterEvent<DamageInfo>(DamageToTarget);
            _damageBuffer = Game.Instance.System<EasyBufferSystem>().Container<DamageInfo>();
            _machine = new DamageSettlementProcedureMachine();
            _machine.AddProcedure(EDamageSettlementProcedure.StartSettlement);
            _machine.AddProcedure(EDamageSettlementProcedure.GainSettlement);
            _machine.AddProcedure(EDamageSettlementProcedure.DefenseSettlement);
            _machine.AddProcedure(EDamageSettlementProcedure.ShieldSettlement);
            _machine.AddProcedure(EDamageSettlementProcedure.HpSettlement);
            _machine.AddProcedure(EDamageSettlementProcedure.EndSettlement);

            _machine.GetState(EDamageSettlementProcedure.StartSettlement)
                .OnEnterCondition(EnterDamageSettlementCondition);
        }

        public static int GetDamage(int damage, params DamageBaseInfo[] damageBaseInfo)
        {
            if (damageBaseInfo == null || damageBaseInfo.Length == 0)
                return damage;
            float damageFloat = damage;
            for (int i = 0,count=damageBaseInfo.Length; i < count; i++)
            {
                var damageBase = damageBaseInfo[i];
                damageFloat += BBPropertyFunc.GetValue(damageBase.Base, damageBase.BaseProperty,damageBase.PropertyValue)*damageBase.Multiplier;
            }
            return Mathf.RoundToInt(damageFloat);
        }
        public static DamageInfo GetDamageInfo(BaseDamageInfo baseDamageInfo,int damage, params DamageBaseInfo[] damageBaseInfo)
        {
            return new DamageInfo()
            {
                Attacker = baseDamageInfo.Attacker,
                Target = baseDamageInfo.Target,
                DamageType = baseDamageInfo.DamageType,
                DamageAct = baseDamageInfo.DamageAct,
                Damage = GetDamage(damage, damageBaseInfo),
            };
        }
        private void DamageToTarget(DamageInfo damageInfo)
        {
            _damageBuffer.Add(damageInfo);
        }

        private void PollingInThisFrameEnd()
        {
            if (!_pollingInThisFrame)
            {
                _pollingInThisFrame = true;
                EasyCoroutine.WaitEndOfFrame(DamagePolling);
            }
        }
        private void DamagePolling()
        {
            for (int i = _damageBuffer.GetCount(); i > 0; i--)
            {
                SettleDamage(_damageBuffer.GetAndRemoveCurrent());
            }
        }

        private bool EnterDamageSettlementCondition(DamageInfo damageInfo)
        {
            var hp = BBPropertyFunc.GetValue(damageInfo.Target, EUnitProperty.Hp.ToString(), EPropertyValue.CurrentValue);
            return hp > 0;
        }
        // 伤害结算
        private void SettleDamage(DamageInfo damageInfo)
        {
            var damage = SettleGain(damageInfo, damageInfo.Damage);
            
            damage = SettleDef(damageInfo, damage);
            
            damage = SettleGuaranteedDamage(damageInfo, damage);

            damage = SettleShield(damageInfo, damage);
            
            SettleHp(damageInfo, damage);
        }
        /// <summary>结算增伤</summary>
        public static float SettleGain(DamageInfo damageInfo,float damage)
        {
            var str = DamageRelatedInfo.Gain(damageInfo.DamageType);
            var gain = BBInBBPropertyEvent.Get<NormalProperty>(damageInfo.Target, str);
            var damageFixed   = gain.Num;
            var damagePercent = gain.Pct;
            return damage * (1 + damagePercent) + damageFixed;
        }
        /// <summary>结算减伤</summary>
        public static float SettleDef(DamageInfo damageInfo,float damage)
        {
            var str = DamageRelatedInfo.Defense(damageInfo.DamageType);
            var defence=BBInBBPropertyEvent.Get<NormalProperty>(damageInfo.Target, str);
            var defenceFixed         = defence.Num;
            var damageDefencePercent = defence.Pct;
            return damage * (1 - damageDefencePercent) - defenceFixed;
        }
        /// <summary>结算保底伤害</summary>
        public static float SettleGuaranteedDamage(DamageInfo damageInfo,float damage)
        {
            var str = DamageRelatedInfo.GuaranteedDamage(damageInfo.DamageType);
            var guaranteedDamage = Mathf.Max(1, BBPropertyFunc.GetValue(damageInfo.Attacker, str, EPropertyValue.CurrentValue));
            return Mathf.Max(damage, guaranteedDamage);
        }
        /// <summary>结算护盾</summary>
        public static float SettleShield(DamageInfo damageInfo,float damage)
        {
            var str = DamageRelatedInfo.Shield(damageInfo.DamageType);
            var shield = BBPropertyFunc.GetValue(damageInfo.Target, str, EPropertyValue.CurrentValue);
            if (shield > 0)
            {
                if (shield >= damage)
                {
                    BBPropertyEvent.SetValue(damageInfo.Target, str, Mathf.RoundToInt(shield - damage));
                }
                else
                {
                    BBPropertyEvent.SetValue(damageInfo.Target, str, 0);
                }
            }
            return Mathf.Clamp(damage-shield,0,float.MaxValue);
        }
        /// <summary>结算血量</summary>
        public static void SettleHp(DamageInfo damageInfo,float damage)
        {
            var str = EUnitProperty.Hp.ToString();
            var hp = BBPropertyFunc.GetValue(damageInfo.Target, str, EPropertyValue.CurrentValue);
            if (hp > 0)
            {
                damage = Mathf.Min(hp, damage);
                BBPropertyEvent.SetValue(damageInfo.Target, str, Mathf.RoundToInt(hp - damage));
            }
        }

        public static bool DamageInfoEquals(DamageInfo damageInfo1, DamageInfo damageInfo2)
        {
            return damageInfo1.Attacker == damageInfo2.Attacker
                   && damageInfo1.Target == damageInfo2.Target
                   && damageInfo1.DamageType == damageInfo2.DamageType;
        }

        public static DamageInfo DamageInfoMerge(DamageInfo damageInfo1, DamageInfo damageInfo2)
        {
            return new DamageInfo()
            {
                Attacker = damageInfo1.Attacker,
                Target = damageInfo1.Target,
                DamageType = damageInfo1.DamageType,
                Damage = damageInfo1.Damage + damageInfo2.Damage
            };
        }
    }

    public enum EDamageSettlementProcedure
    {
        StartSettlement,
        GainSettlement,
        DefenseSettlement,
        ShieldSettlement,
        HpSettlement,
        EndSettlement,
    }

    public class DamageSettlementProcedureMachine : AProcedureMachine<EDamageSettlementProcedure,
        UniversalEasyProcedure<DamageSettlementProcedureMachine,DamageInfo>, DamageInfo>
    {
        
    }
}