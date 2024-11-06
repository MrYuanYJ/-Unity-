using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework
{
    public enum EUnitProperty
    {
        Hp,
        Mp,
        Atk,
        Def,
        Shield,
        MoveSpeed,
        JumpForce,
        Luck,
        Exp,
        Gold,
        Level,
    }
    public class BBPropertyEntity: AMonoEntity<BBProperty>
    {
        public override IStructure Structure => Root.Structure;

        public BlackBoard BlackBoard => Mono.blackBoard;


        protected override void OnInit()
        {
        }

        public RoleProperties GetProperty(string key)
        {
            return BlackBoard.Get<RoleProperties>(key);
        }

        public RoleProperties GetOrAddRoleProperty(string key)
        {
            if (!BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty = new RoleProperties(0, 0);
                BlackBoard.Add(key, roleProperty);
            }
            return roleProperty;
        }

        public RoleProperties SetProperty(string key, RoleProperties value)
        {
            BlackBoard.Add(key, value);
            return value;
        }

        public float GetValue(string key,EPropertyValue propertyValue)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                return propertyValue switch
                {
                    EPropertyValue.CurrentValue => roleProperty.Value,
                    EPropertyValue.MaxValue => roleProperty.MaxValue,
                    EPropertyValue.LostValue=> roleProperty.GetLostValue(),
                    EPropertyValue.Percent => roleProperty.GetCurrentPercent(),
                    EPropertyValue.ReversePercent => roleProperty.GetCurrentLostPercent(),
                    _ => throw new ArgumentOutOfRangeException(nameof(propertyValue), propertyValue, null)
                };
            }
            return 0;
        }

        public void SetValue(string key, int value)
        {
            BlackBoard.Add(key, new RoleProperties(value, 0));
        }

        public void SetPercent(string key, float percent)
        {
            if(BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.SetValuePercent(percent);
            }
        }

        public void AddPercent(string key, float percent)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.AddValuePercent(percent);
            }
        }

        public void SetPropertyUp(string key,RolePropertyData data)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.SetPropertyUp(data);
            }
        }

        public void AddPropertyUp(string key, RolePropertyData data)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.AddPropertyUp(data);
            }
        }

        public void SetBase(string key, int value,int percent)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.SetBase(value,percent);
            }
        }

        public void AddBase(string key, int value, int percent)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.AddBase(value, percent);
            }
        }

        public void SetFinal(string key, int value, int percent)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.SetFinal(value, percent);
            }
        }

        public void AddFinal(string key, int value, int percent)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                roleProperty.AddFinal(value, percent);
            }
        }

        public int GetBaseAdd(string key)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                return roleProperty.GetBaseAdd();
            }
            return 0;
        }

        public float GetBasePercent(string key)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                return roleProperty.GetBasePct();
            }
            return 0;
        }

        public int GetFinalAdd(string key)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                return roleProperty.GetFinalAdd();
            }
            return 0;
        }

        public float GetFinalPercent(string key)
        {
            if (BlackBoard.TryGetValue<RoleProperties>(key, out var roleProperty))
            {
                return roleProperty.GetFinalPct();
            }
            return 0;
        }
     }

    
    [System.Serializable]
    public struct RecoveryData
    {
        public int RecoveryValue;
        [Range(0, 1)] public float RecoveryPercent;
    }
    public struct BBPropertyEvent
    {
        public static void SetValue(IUnitEntity unit, string key, int value)=>unit.GetEntity<BBPropertyEntity>()?.SetValue(key, value);
        public static void SetPercent(IUnitEntity unit, string key, float percent)=>unit.GetEntity<BBPropertyEntity>()?.SetPercent(key, percent);
        public static void AddPercent(IUnitEntity unit, string key, float percent)=>unit.GetEntity<BBPropertyEntity>()?.AddPercent(key, percent);
        public static void SetPropertyUp(IUnitEntity unit, string key, RolePropertyData data)=>unit.GetEntity<BBPropertyEntity>()?.SetPropertyUp(key, data);
        public static void AddPropertyUp(IUnitEntity unit, string key, RolePropertyData data)=>unit.GetEntity<BBPropertyEntity>()?.AddPropertyUp(key, data);
        public static void SetBase(IUnitEntity unit, string key, int value, int percent)=>unit.GetEntity<BBPropertyEntity>()?.SetBase(key, value, percent);
        public static void AddBase(IUnitEntity unit, string key, int value, int percent)=>unit.GetEntity<BBPropertyEntity>()?.AddBase(key, value, percent);
        public static void SetFinal(IUnitEntity unit, string key, int value, int percent)=>unit.GetEntity<BBPropertyEntity>()?.SetFinal(key, value, percent);
        public static void AddFinal(IUnitEntity unit, string key, int value, int percent)=>unit.GetEntity<BBPropertyEntity>()?.AddFinal(key, value, percent);
    }

    public struct BBPropertyFunc
    {
        public static RoleProperties GetRoleProperty(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetProperty(key);
        public static RoleProperties GetOrAddRoleProperty(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetOrAddRoleProperty(key);
        public static RoleProperties SetRoleProperty(IUnitEntity unit, string key, RoleProperties value) => unit.GetEntity<BBPropertyEntity>()?.SetProperty(key, value);
        public static float GetValue(IUnitEntity unit, string key, EPropertyValue propertyValue) => unit.GetEntity<BBPropertyEntity>()?.GetValue(key, propertyValue) ?? 0;
        public static int GetBaseAdd(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetBaseAdd(key) ?? 0;
        public static float GetBasePercent(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetBasePercent(key) ?? 0;
        public static int GetFinalAdd(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetFinalAdd(key) ?? 0;
        public static float GetFinalPercent(IUnitEntity unit, string key) => unit.GetEntity<BBPropertyEntity>()?.GetFinalPercent(key) ?? 0;
        
        public static string GetRecoveryStr(EUnitProperty property) => $"{property.ToString()}Recovery";
    }

    public struct BBInBBPropertyEvent
    {
        public static void Set(IUnitEntity unit, string key, object value) =>
            unit.GetEntity<BBPropertyEntity>()?.BlackBoard.Add(key, value);
        public static void Set<T>(IUnitEntity unit, string key, T value) =>
            unit.GetEntity<BBPropertyEntity>()?.BlackBoard.Add(key, value);
        public static T Get<T>(IUnitEntity unit, string key)
        {
            if (unit.TryGetEntity<BBPropertyEntity>(out var bbPropertyEntity))
            {
                return bbPropertyEntity.BlackBoard.Get<T>(key);
            }

            return default;
        }
        public static object Get(IUnitEntity unit, string key)
        {
            if (unit.TryGetEntity<BBPropertyEntity>(out var bbPropertyEntity))
            {
                return bbPropertyEntity.BlackBoard.Get(key);
            }

            return null;
        }
        public static bool TryGet<T>(IUnitEntity unit, string key, out T value)
        {
            if (unit.TryGetEntity<BBPropertyEntity>(out var bbPropertyEntity))
            {
                return bbPropertyEntity.BlackBoard.TryGetValue(key, out value);
            }

            value = default;
            return false;
        }
        public static bool TryGet(IUnitEntity unit, string key, out object value)
        {
            if (unit.TryGetEntity<BBPropertyEntity>(out var bbPropertyEntity))
            {
                return bbPropertyEntity.BlackBoard.TryGetValue(key, out value);
            }

            value = null;
            return false;
        }
        public static void Remove(IUnitEntity unit, string key)
        {
            if (unit.TryGetEntity<BBPropertyEntity>(out var bbPropertyEntity))
            {
                bbPropertyEntity.BlackBoard.Remove(key);
            }
        }
    }
}
