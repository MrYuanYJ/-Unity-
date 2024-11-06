using EasyFramework.EasySystem;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework.ClimbEntities
{
    public class Climb2DCtrlEntity: AMonoEntity<Climb2DCtrl>
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
                UnitFunc.MoveDir2D.InvokeFunc((RoleEntity) Root).Modify(y: direction.y));
        }
    }
}