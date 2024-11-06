using Sirenix.OdinInspector;

namespace EasyFramework
{
    public class HoldItemAble: AMonoEntityCarrier
    {
        public Weapon Weapon;

        [Button]
        private void Start()
        {
            var holdItemAbleEntity = (HoldItemAbleEntity) Entity;
            holdItemAbleEntity.HoldItem((WeaponEntity)Weapon.Entity);
        }
    }
}