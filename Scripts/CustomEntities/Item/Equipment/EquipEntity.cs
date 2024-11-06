using System;

namespace EasyFramework
{
    public class EquipEntity: ItemEntity,IEquip
    {
        protected bool IsEquipped => ((IEquip) this).IsEquipped;
        bool IEquip.IsEquipped { get; set; }

        void IEquip.OnEquip()
        {
            /*var itemObjHandle = ItemFunc.GetItemObjByItemId.InvokeFunc(ItemData.ItemId);
            itemObjHandle.Completed += () =>
            {
                ((IEntity) this).SetBindObj(itemObjHandle.Result);
                OnEquip();
            };*/
        }

        void IEquip.OnRemove()
        {
            ((IEntity)this).SetBindObj(null);
            OnRemove();
        }

        protected virtual void OnEquip(){}
        protected virtual void OnRemove(){}
    }
}