using System;
using UnityEngine;

namespace EasyFramework
{
    public interface IItem: IUnitEntity
    {
        long ItemUnitId { get; }
        long IUnitEntity.UnitId => ItemUnitId;
        int ItemId { get; }
        string ItemName { get; }
        Enum ItemType { get; }
        Enum IUnitEntity.UnitType => ItemType;
        int InventoryIndex { get; }
        IUnitEntity User { get; set; }
        int StackCount { get; set; }
        int MaxStackCount { get; set; }
        bool IsGained => User!= null;

        bool Gain(IUnitEntity user)
        {
            if (IsGained)
                return false;
            User = user;
            OnGain();
            return true;
        }
        
        bool Lose()
        {
            if (!IsGained)
                return false;
            User = null;
            OnLose();
            return true;
        }

        int Stack(int count)
        {
            int stackCount=Mathf.Min(count, MaxStackCount - StackCount);
            if (stackCount > 0)
            {
                StackCount += stackCount;
                OnStack(stackCount);
            }
            return stackCount;
        }
        
        int Unstack(int count)
        {
            int unstackCount=Mathf.Min(count, StackCount);
            if (unstackCount > 0)
            {
                StackCount -= unstackCount;
                OnUnstack(unstackCount);
                if (StackCount == 0)
                    Lose();
            }
            return unstackCount;
        }

        protected void OnGain();
        protected void OnLose();
        protected void OnStack(int count);
        protected void OnUnstack(int count);
    }
}