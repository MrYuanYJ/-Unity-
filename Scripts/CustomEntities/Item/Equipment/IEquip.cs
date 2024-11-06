namespace EasyFramework
{
    public interface IEquip: IItem
    {
        bool IsEquipped { get; protected set; }

        public void Equip(RoleEntity user)
        {
            if(IsEquipped)
                return;
            IsEquipped = true;
            OnEquip();
        }
        public void Remove()
        {
            if(!IsEquipped)
                return;
            IsEquipped = false;
            OnRemove();
        }
        protected void OnEquip();
        protected void OnRemove();
    }
}