using System;

namespace EasyFramework
{
    public class WeaponEntity: EquipEntity
    {
        private CoroutineHandle _weaponCoroutineHandle;
        private CoroutineHandle _weaponCdCoroutineHandle;
        private WeaponProcedureMachine _weaponMachine;
        public float windUp;
        public float attack;
        public float windDown;
        public float cooldown;

        public override IStructure Structure => ItemStructure.TryRegister();

        protected override void OnActive()
        {
            base.OnActive();
            _weaponMachine.AddProcedure(EWeaponProcedure.WindUp);
            _weaponMachine.AddProcedure(EWeaponProcedure.Attack);
            _weaponMachine.AddProcedure(EWeaponProcedure.WindDown);
            _weaponMachine.AddProcedure(EWeaponProcedure.EnterCoolDown);
            _weaponMachine.GetState(EWeaponProcedure.WindUp).OnEnter(OnWindUp);
            _weaponMachine.GetState(EWeaponProcedure.Attack).OnEnter(OnAttack);
            _weaponMachine.GetState(EWeaponProcedure.WindDown).OnEnter(OnWindDown);
            _weaponMachine.GetState(EWeaponProcedure.EnterCoolDown).OnEnter(OnEnterCoolDown);
            WeaponFunc.GetWeaponMachine.RegisterFunc(this, GetWeaponMachine);
        }

        protected override void OnUnActive()
        {
            base.OnUnActive();
            _weaponMachine.RemoveProcedure(EWeaponProcedure.WindUp);
            _weaponMachine.RemoveProcedure(EWeaponProcedure.Attack);
            _weaponMachine.RemoveProcedure(EWeaponProcedure.WindDown);
            _weaponMachine.RemoveProcedure(EWeaponProcedure.EnterCoolDown);
            WeaponFunc.GetWeaponMachine.UnRegisterFunc(this, GetWeaponMachine);
        }

        private async void OnWindUp(WeaponEntity weapon)
        {
            _weaponCoroutineHandle= EasyCoroutine.Delay(windUp, false);
            await _weaponCoroutineHandle;
            _weaponMachine.NextProcedure(this);
        }
        
        private async void OnAttack(WeaponEntity weapon)
        {
            _weaponCoroutineHandle= CoroutineHandle.Fetch();
            await _weaponCoroutineHandle;
            _weaponMachine.NextProcedure(this);
        }
        
        private async void OnWindDown(WeaponEntity weapon)
        {
            _weaponCoroutineHandle= EasyCoroutine.Delay(windDown, false);
            await _weaponCoroutineHandle;
            _weaponMachine.NextProcedure(this);
        }

        private async void OnEnterCoolDown(WeaponEntity weapon)
        {
            _weaponCdCoroutineHandle= EasyCoroutine.Delay(cooldown, false);
            _weaponMachine.NextProcedure(this);
            await _weaponCdCoroutineHandle;
            _weaponCdCoroutineHandle = null;
        }


        public WeaponProcedureMachine GetWeaponMachine() => _weaponMachine;
        protected override void OnEquip()
        {
            UnitEvent.Attack.RegisterEvent(User, Attack);
            UnitEvent.AttackEnd.RegisterEvent(User, AttackEnd);
        }

        protected override void OnRemove()
        {
            UnitEvent.Attack.UnRegisterEvent(User, Attack);
            UnitEvent.AttackEnd.UnRegisterEvent(User, AttackEnd);
        }

        void Attack()
        {
            if (_weaponMachine.CurrentState == EWeaponProcedure.None
                || _weaponCdCoroutineHandle==null)
                _weaponMachine.ReStart(this);
        }
        void AttackEnd()
        {
            if (_weaponMachine.CurrentState == EWeaponProcedure.Attack)
                CancelCurrentProcedure();
        }
        void CancelCurrentProcedure()
        {
            _weaponCoroutineHandle.Cancel();
        }
    }

    public enum EWeaponProcedure
    {
        None,
        WindUp,
        Attack,
        WindDown,
        EnterCoolDown,
        Reload,
    }

    public enum EAttackProcedure
    {
        None,
        Attack,
        InAttackInterval,
    }

    [Serializable]
    public class WeaponProcedureMachine : AProcedureMachine<EWeaponProcedure,
        UniversalEasyProcedure<WeaponProcedureMachine, WeaponEntity>, WeaponEntity>
    {
    }
    [Serializable]
    public class AttackProcedureMachine : AProcedureMachine<EWeaponProcedure,
        UniversalEasyProcedure<WeaponProcedureMachine, WeaponEntity>, WeaponEntity>
    {
    }
}