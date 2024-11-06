using EasyFramework.EasySystem;

namespace EasyFramework.JumpEntities
{
    public class Jump2DCtrlEntity: AMonoEntity<Jump2DCtrl>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public UnitJump2DEntity Jump2DEntity;
        protected override void OnInit()
        {
            Jump2DEntity = (UnitJump2DEntity) Parent;
        }
        protected override void OnActive()
        {
            EasyInputSetting.Instance.InputStateDict[EInput.Jump].IsPressed.Register(SetIsJumpByInput);
        }
        protected override void OnUnActive()
        {
            EasyInputSetting.Instance.InputStateDict[EInput.Jump].IsPressed.UnRegister(SetIsJumpByInput);
        }

        private void SetIsJumpByInput(bool isJump)
        {
            Jump2DEntity.Mono.isJump.Value = isJump;
        }
    }
}