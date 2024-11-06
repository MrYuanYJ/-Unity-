using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class ItemStructure: AStructure<ItemStructure>
    {
        protected override void OnInit()
        {
            this.System<ItemSystem>();
        }
    }

    public struct ItemEvent
    {
        public sealed class GainItem: AEventIndex<GainItem,IUnitEntity>{}
    }

    public struct ItemFunc
    {
        public sealed class CreateItemByItemId: AFuncIndex<CreateItemByItemId,int,IItem>{}
        public sealed class GetItemInInventory: AFuncIndex<GetItemInInventory,long,IItem>{}
        public sealed class GetItemsInInventory: AFuncIndex<GetItemsInInventory,Predicate<IItem>,IEnumerator<IItem>>{}
    }
    public struct WeaponEvent
    {
        
    }

    public struct WeaponFunc
    {
        public sealed class GetWeaponMachine: AFuncIndex<GetWeaponMachine,WeaponProcedureMachine>{}
        
    }
}