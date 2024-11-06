using System;
using EasyFramework.EasySystem;

namespace EasyFramework
{
    public enum EDamage
    {
        Physical,
        Fire,
        Ice,
        Lightning,
        Dark,
        Poison,
        Bleed,
        Holy,
        Real
    }
    public enum EDamageDef
    {
        PhysicalDef,
        FireDef,
        IceDef,
        LightningDef,
        DarkDef,
        PoisonDef,
        BleedDef,
        HolyDef,
    }
    public enum EDamageAct
    {
        None,
        Attack,
        Skill
    }
    public enum EPropertyValue
    {
        CurrentValue,
        MaxValue,
        LostValue,
        Percent,
        ReversePercent
    }
    
    public struct DamageBaseInfo
    {
        /// <summary>伤害数值源</summary>
        public IUnitEntity Base;
        /// <summary>数值索引名</summary>
        public string BaseProperty;
        /// <summary>数值源类型</summary>
        public EPropertyValue PropertyValue;
        /// <summary>数值倍率</summary>
        public float Multiplier;
    }
    public struct BaseDamageInfo
    {
        public IUnitEntity Attacker;
        public IUnitEntity Target;
        public EDamage DamageType;
        public EDamageAct DamageAct;
    }
    public struct DamageInfo: IBufferAble
    {
        /// <summary>伤害来源</summary>
        public IUnitEntity Attacker;
        /// <summary>伤害目标</summary>
        public IUnitEntity Target;
        /// <summary>伤害类型</summary>
        public EDamage DamageType;
        /// <summary>造成伤害的行为</summary>
        public EDamageAct DamageAct;
        /// <summary>伤害值</summary>
        public int Damage;
        public float ExistTime { get; set; }
    }

}