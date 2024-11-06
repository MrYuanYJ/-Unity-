using System;
using System.Threading.Tasks;

namespace EasyFramework
{
    public class MagazineEntity: AMonoEntity<Magazine>
    {
        public override IStructure Structure => ItemStructure.TryRegister();
        private WeaponEntity WeaponEntity { get; set; }
        private CoroutineHandle _reloadCoroutineHandle;
        private RoleProperties _totalAmmo;
        private RoleProperties _magazineAmmo;
        private RoleProperties _reloadSpeed;

        protected override void OnInit()
        {
            WeaponEntity = (WeaponEntity) Root;
            _totalAmmo = BBPropertyFunc.SetRoleProperty(WeaponEntity, EMagzineProperty.TotalAmmo.ToString(),new RoleProperties(Mono.totalAmmo));
            _magazineAmmo = BBPropertyFunc.SetRoleProperty(WeaponEntity, EMagzineProperty.MagazineAmmo.ToString(),new RoleProperties(Mono.magazineAmmo));
            _reloadSpeed = BBPropertyFunc.SetRoleProperty(WeaponEntity, EMagzineProperty.ReloadSpeed.ToString(),new RoleProperties(Mono.reloadSpeed));
        }

        protected override void OnActive()
        {
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            weaponMachine.AddState(EWeaponProcedure.Reload);
            weaponMachine.GetState(EWeaponProcedure.Reload)
                .OnEnter(OnReload)
                .OnExit(StopReload);
                
            weaponMachine.GetState(EWeaponProcedure.Attack)
                .OnEnterCondition(AmmunitionTest);

            UnitEvent.Attack.RegisterEvent(TryAttackAndStopReload);
        }

        protected override void OnUnActive()
        {
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            weaponMachine.RemoveState(EWeaponProcedure.Reload);
            weaponMachine.GetState(EWeaponProcedure.Attack)
                .RemoveEnterCondition(AmmunitionTest);
            
            UnitEvent.Attack.UnRegisterEvent(TryAttackAndStopReload);
        }

        private async void OnReload(WeaponEntity weaponEntity)
        {
            if(_totalAmmo.Value==0)
                return;
            switch (Mono.reloadType)
            {
                case EReloadType.Once:
                    await ReloadOnce();
                    break;
                case EReloadType.Repeat:
                    await ReloadRepeat();
                    break;
            }
            
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            weaponMachine.ExitCurrentState(WeaponEntity);
        }

        private async Task ReloadOnce()
        {
            float loadPct = 1;
            var ammo = _totalAmmo.Value;
            if (ammo > 0)
            {
                _reloadCoroutineHandle = EasyCoroutine.Delay(Mono.singleReloadDuration, false);
                var maxMagazineAmmo = _magazineAmmo.MaxValue;
                var currentMagazineAmmo = _magazineAmmo.Value;
                var needAmmo = maxMagazineAmmo - currentMagazineAmmo;
                Action onCompleted = null;
                if (ammo >= needAmmo)
                    onCompleted += () => _totalAmmo.AddValue(-needAmmo);
                else
                {
                    loadPct = (float) (ammo + currentMagazineAmmo) / maxMagazineAmmo;
                    onCompleted += () => _totalAmmo.SetValuePercent(0);
                }

                await _reloadCoroutineHandle.OnCompleted(() =>
                {
                    onCompleted?.Invoke();
                    _magazineAmmo.SetValuePercent(loadPct);
                });
            }
            _reloadCoroutineHandle = null;
        }

        private async Task ReloadRepeat()
        {
            bool reloadCancel = false;
            while (_magazineAmmo.CurrentPct.Value < 1
                   && !reloadCancel)
            {
                _reloadCoroutineHandle = EasyCoroutine.Delay(Mono.singleReloadDuration, false)
                    .OnCompleted
                    (() => _magazineAmmo
                            .AddValue(Mono.singleReloadAmmoCount)
                            .AddValuePercent(Mono.singleReloadAmmoPct)
                    )
                    .OnCanceled(() => reloadCancel = true);
                await _reloadCoroutineHandle;
            }
            _reloadCoroutineHandle = null;
        }
        private bool AmmunitionTest(WeaponEntity weaponEntity)
        {
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            if (Mono.singleConsumeAmmo > _magazineAmmo.Value)
            {
                weaponMachine.ChangeState(EWeaponProcedure.Reload, weaponEntity);
                return false;
            }

            _magazineAmmo.AddValue(-Mono.singleConsumeAmmo);
            return true;
        }
        private void StopReload()
        {
            _reloadCoroutineHandle.Cancel();
            _reloadCoroutineHandle = null;
        }
        private void StopReload(WeaponEntity weaponEntity)=>StopReload();
        private void TryAttackAndStopReload()
        {
            var weaponMachine = WeaponFunc.GetWeaponMachine.InvokeFunc(WeaponEntity);
            if (weaponMachine.CurrentState == EWeaponProcedure.Reload
                && Mono.singleConsumeAmmo <= _magazineAmmo.Value)
            {
                StopReload();
                weaponMachine.ReStart(WeaponEntity);
            }
        }
    }
}