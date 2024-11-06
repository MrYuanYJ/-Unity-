using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public enum EUnit
    {
        Player,
        Enemy,
        Npc,
        Object,
        Item,
    }
    [ParentEnum(EUnit.Item)]
    public enum EItem
    {
        Normal,
        Usable,
        Equipment,
    }
    [ParentEnum(EItem.Equipment)]
    public enum EEquipment
    {
        Weapon,
        Other,
    }
    [ParentEnum(EItem.Usable)]
    public enum EUsableItem
    {
        Treasure,
        Drug
    }
    [ParentEnum(EUnit.Object)]
    public enum EObject
    {
        Bullet,
    }
    public class UnitSystem : ASystem
    {
        private Dictionary<long, IUnitEntity> AllUnit { get; set; } = new();
        private Dictionary<Enum, HashSet<IUnitEntity>> UnitGroup { get; set; } = new();

        protected override void OnActive()
        {
            UnitEvent.Register.RegisterEvent(AddUnit);
            UnitEvent.Register.RegisterEvent(RemoveUnit);
        }

        protected override void OnUnActive()
        {
            UnitEvent.Register.UnRegisterEvent(AddUnit);
            UnitEvent.Register.UnRegisterEvent(RemoveUnit);
        }

        public void AddUnit(IUnitEntity unit)
        {
            AllUnit.TryAdd(unit.UnitId, unit);
            if (!UnitGroup.TryGetValue(unit.UnitType, out var group))
            {
                group = new();
                UnitGroup[unit.UnitType] = group;
            }
            group.Add(unit);
        }

        public IUnitEntity GetUnit(long unitId)
        {
            return AllUnit.GetValueOrDefault(unitId);
        }

        public void RemoveUnit(IUnitEntity unit)
        {
            AllUnit.Remove(unit.UnitId);
            UnitGroup.GetValueOrDefault(unit.UnitType).Remove(unit);
        }

        public void RemoveUnit(long unitId)
        {
            AllUnit.Remove(unitId, out var unit);
            UnitGroup.GetValueOrDefault(unit.UnitType).Remove(unit);
        }
    }
}