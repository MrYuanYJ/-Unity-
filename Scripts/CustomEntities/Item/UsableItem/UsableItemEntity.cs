namespace EasyFramework
{
    public class UsableItemEntity: ItemEntity, IUsableItem 
    {
        public int MaxUseCount { get; set; }
        protected int UseCount => ((IUsableItem)this).UseCount;
        int IUsableItem.UseCount { get; set; }
        public float Cooldown { get; set; }
        protected bool IsInCooldown => ((IUsableItem)this).CdHandle!=null;
        CoroutineHandle IUsableItem.CdHandle { get; set; }

        void IUsableItem.OnUse(int useCount)=> OnUse(useCount);
        void IUsableItem.OnCharge(int chargeCount)=> OnCharge(chargeCount);
        
        protected virtual void OnUse(int useCount){}
        protected virtual void OnCharge(int chargeCount){}
    }
}