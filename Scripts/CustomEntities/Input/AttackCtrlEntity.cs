using EasyFramework.EasySystem;

namespace EasyFramework.Input
{
    public class AttackCtrlEntity: AMonoEntity<AttackCtrl>
    {
        public override IStructure Structure => UnitStructure.TryRegister();

        protected override void OnActive()
        {
            EasyInputSetting.Instance.InputStateDict[EInput.Attack].IsPressed.Register(AttackByInput);
            EasyInputSetting.Instance.InputStateDict[EInput.Attack].TimePressed.Register(AttackHoldByInput);
        }
        protected override void OnUnActive()
        {
            EasyInputSetting.Instance.InputStateDict[EInput.Attack].IsPressed.UnRegister(AttackByInput);
            EasyInputSetting.Instance.InputStateDict[EInput.Attack].TimePressed.UnRegister(AttackHoldByInput);
        }
        private void AttackByInput(bool isAttack)
        {
            if (!isAttack)
                return;
            UnitEvent.Attack.InvokeEvent((RoleEntity)Root);
        }

        private void AttackHoldByInput(float holdTime)
        {
            
        }
    }
}