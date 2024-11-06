using System;
using System.Collections.Generic;
using EXFunctionKit;

namespace EasyFramework
{
    public class ItemSystem: ASystem
    {
        private Dictionary<int, ItemSOBase> _itemCfg;
        protected override void OnInit()
        {
            _itemCfg = ItemTableSO.Instance.items.AsDictionary(item => item.ItemID);
            ItemFunc.CreateItemByItemId.RegisterFunc(CreateItemByItemId);
        }

        protected override void OnDispose(bool usePool)
        {
            ItemFunc.CreateItemByItemId.UnRegisterFunc(CreateItemByItemId);
        }

        IItem CreateItemByItemId(int itemId)
        {
            var itemCfg = _itemCfg.GetValueOrDefault(itemId);
            var itemEntity=ReferencePool.Fetch<ItemEntity>();
            if (itemCfg != null)
            {
                itemEntity.SetBaseDataBySO(itemCfg);
            }

            return itemEntity;
        }
    }
}