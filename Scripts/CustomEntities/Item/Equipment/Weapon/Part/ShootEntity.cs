using UnityEngine;

namespace EasyFramework
{
    public class ShootEntity: AMonoEntity<Shoot>
    {
        public override IStructure Structure => ItemStructure.TryRegister();
        public WeaponEntity WeaponEntity { get; set; }

        protected override void OnInit()
        {
            WeaponEntity = (WeaponEntity) Root;
        }

        protected override void OnActive()
        {
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            weaponMachine.GetState(EWeaponProcedure.Attack)
                .OnEnter(OnEnterAttack);
        }
        protected override void OnUnActive()
        {
            
        }

        private void OnEnterAttack(WeaponEntity weaponEntity)
        {
            Debug.Log("OnEnterAttack");
            UnitEvent.AttackEnd.InvokeEvent(weaponEntity.User);
        }
    }
}