using System;
using System.Collections.Generic;
using EXFunctionKit;

namespace EasyFramework
{
    public class InventoryEntity : AMonoEntity<Inventory>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private Dictionary<long, IItem> _itemDict = new();
        private IItem[] _items = new IItem[]{};
        ESProperty<bool> isLocked = new(false);

        protected override void OnActive()
        {
            base.OnActive();
            isLocked.Register(SetLocked);
            var unitEntity = (IUnitEntity) Root;
            ItemFunc.GetItemInInventory.RegisterFunc(unitEntity, GetItemInInventory);
            ItemFunc.GetItemsInInventory.RegisterFunc(unitEntity, GetItemsInInventory);
        }

        protected override void OnUnActive()
        {
            base.OnUnActive();
            isLocked.UnRegister(SetLocked);
            var unitEntity = (IUnitEntity) Root;
            ItemFunc.GetItemInInventory.UnRegisterFunc(unitEntity, GetItemInInventory);
            ItemFunc.GetItemsInInventory.UnRegisterFunc(unitEntity, GetItemsInInventory);
        }

        void RefreshInventory()
        {
            _itemDict.Clear();
            _itemDict = _items.AsDictionary(item => item.UnitId, _itemDict);
            if (_items.Length < Mono.capacity)
                Array.Resize(ref _items, Mono.capacity);
            foreach (var item in _itemDict.Values)
            {
                _items[item.InventoryIndex] = item;
            }
        }

        void SetLocked(bool value)
        {

        }

        public void GainItem(int itemId, int count)
        {
            var unitEntity = (IUnitEntity) Root;
            while (count > 0)
            {
                
            }
            for (int i = 0,length= _items.Length; i < length; i++)
            {
                if (_items[i].ItemId == itemId)
                {
                    
                }
            }
            var item = ItemFunc.CreateItemByItemId.InvokeFunc(itemId);
            item.Gain(unitEntity);
            item.Stack(count);
        }

        private void GainItem(IItem item)
        {
            
        }

        private int FindFirstItemIndex(int itemId,int startIndex)
        {
            for (int length= _items.Length; startIndex < length; startIndex++)
            {
                if (_items[startIndex].ItemId == itemId)
                {
                    return startIndex;
                }
            }
            return -1;
        }
        private IItem GetItemInInventory(long itemUnitId)
        {
            return _itemDict.GetValueOrDefault(itemUnitId);
        }

        private IEnumerator<IItem> GetItemsInInventory(Predicate<IItem> filter)
        {
            foreach (var item in _itemDict.Values)
            {
                if (filter != null)
                {
                    if (filter(item))
                        yield return item;
                }
                else
                    yield return item;
            }
        }
        public void AddItem(IItem item)=>_itemDict.Add(item.UnitId, item);
    }
}