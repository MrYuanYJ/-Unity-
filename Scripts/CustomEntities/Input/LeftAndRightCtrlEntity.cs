using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class LeftAndRightCtrlEntity: AMonoEntity<LeftAndRightCtrl>
    {
        public override IStructure Structure => UnitStructure.TryRegister();

        protected override void OnActive()
        {
            EasyInputSetting.Instance.MoveInput.Register(SetDirectionByInput);
        }

        protected override void OnUnActive()
        {
            EasyInputSetting.Instance.MoveInput.UnRegister(SetDirectionByInput);
        }
        private void SetDirectionByInput(Vector2 direction)
        {
            UnitEvent.SetMoveDir2D.InvokeEvent((RoleEntity) Root,
                UnitFunc.MoveDir2D.InvokeFunc((RoleEntity) Root)
                    .Modify(x: direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0));
        }
    }
}