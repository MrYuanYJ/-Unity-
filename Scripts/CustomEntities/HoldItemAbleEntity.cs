
namespace EasyFramework
{
    public class HoldItemAbleEntity: AMonoEntity<HoldItemAble>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public IEquip CurrentHoldItem;

        public void HoldItem(IEquip item)
        {
            item.Equip((RoleEntity)Root);
            CurrentHoldItem = item;
        }
        
        public void ReleaseItem()
        {
            if (CurrentHoldItem!= null)
            {
                CurrentHoldItem.Remove();
                CurrentHoldItem = null;
            }
        }
    }
}