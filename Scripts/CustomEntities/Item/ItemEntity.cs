using System;

namespace EasyFramework
{
    public class ItemEntity: AEntity, IItem
    {
        public long ItemUnitId { get; private set; }
        public int ItemId { get; private set; }
        public string ItemName { get; private set; }
        public virtual Enum ItemType { get; private set; } = EItem.Normal;
        public int InventoryIndex { get; set; }
        public IUnitEntity User { get; set; }
        public int StackCount { get; set; }
        public int MaxStackCount { get; set; }
        public EasyEventDic EventDic { get; } = new EasyEventDic();
        public EasyFuncDic FuncDic { get; } = new EasyFuncDic();

        public override IStructure Structure => ItemStructure.TryRegister();

        public void SetBaseDataBySO(ItemSOBase itemSoBase)
        {
            ItemId = itemSoBase.ItemID;
            ItemName = itemSoBase.ItemName;
            ItemType = itemSoBase.ItemType;
            StackCount = 0;
            MaxStackCount = itemSoBase.MaxCount;
        }
        protected override void OnInit()
        {
            ItemUnitId = EasyID.GetNewID.InvokeFunc();
            
            UnitEvent.Register.InvokeEvent(this);
            UnitFunc.GetBelongUnit.RegisterFunc(this, GetBelongUnitEntity);
        }
        protected override void OnDispose(bool usePool)
        {
            UnitEvent.UnRegister.InvokeEvent(this);
            UnitFunc.GetBelongUnit.UnRegisterFunc(this, GetBelongUnitEntity);
        }

        void IItem.OnGain()=> OnGain();
        void IItem.OnLose()=> OnLose();
        void IItem.OnStack(int count)=> OnStack(count);
        void IItem.OnUnstack(int count)=> OnUnstack(count);

        
        protected virtual void OnGain(){}
        protected virtual void OnLose(){}
        protected virtual void OnStack(int count){}
        protected virtual void OnUnstack(int count){}
        protected virtual IUnitEntity GetBelongUnitEntity()=> User;

    }
}