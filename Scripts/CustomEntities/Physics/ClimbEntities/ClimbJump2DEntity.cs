
using UnityEngine;

namespace EasyFramework.ClimbEntities
{
    public class ClimbJump2DEntity: AMonoEntity<ClimbJump2D>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public UnitClimb2DEntity ClimbEntity;
        public Vector2 JumpDir;

        protected override void OnInit()
        {
            ClimbEntity = (UnitClimb2DEntity) Parent;
        }

        protected override void OnActive()
        {
            UnitFunc.JumpCondition.RegisterFunc((RoleEntity)Root,CheckClimbJump);
            ClimbState.Instance.OnEnter(EnterClimbState);
            ClimbState.Instance.OnExit(CoyoteClimbJump);
        }

        protected override void OnUnActive()
        {
            UnitFunc.JumpCondition.UnRegisterFunc((RoleEntity)Root,CheckClimbJump);
            ClimbState.Instance.RemoveEnter(EnterClimbState);
            ClimbState.Instance.RemoveEnter(CoyoteClimbJump);
        }

        private void EnterClimbState(RoleEntity roleEntity)
        {
            if (ClimbEntity.UnitStateCheck2DEntity.WallNormal.x > 0)
                JumpDir= Quaternion.Euler(0, 0, Mono.JumpAngle) * ClimbEntity.UnitStateCheck2DEntity.WallNormal;
            else
                JumpDir= Quaternion.Euler(0, 0, -Mono.JumpAngle) * ClimbEntity.UnitStateCheck2DEntity.WallNormal;
        }
        private bool CheckClimbJump()
        {
            if (UnitFunc.CurrentMovementState.InvokeFunc((RoleEntity) Root) == EMovementState.Climb)
            {
                UnitEvent.JumpStart
                    .RegisterEvent((RoleEntity) Root, ClimbJumpSet)
                    .OnlyPlayOnce();
                UnitEvent.JumpEnd
                    .RegisterEvent((RoleEntity) Root, ResetHorizontalAndVerticalMoveAble)
                    .OnlyPlayOnce();
                return true;
            }
            return false;
        }
        private void ClimbJumpSet()
        {
            UnitEvent.SetJumpDir2D.InvokeEvent((RoleEntity) Root, JumpDir);
            UnitEvent.BanMove.InvokeEvent((RoleEntity) Root);
        }

        private void ResetHorizontalAndVerticalMoveAble()=> UnitEvent.ResetHorizontalAndVerticalMoveAble.InvokeEvent((RoleEntity) Root);

        private void CoyoteClimbJump(RoleEntity roleEntity)
        {
            var handle = UnitFunc.JumpCoyoteTimer.BaseInvoke(roleEntity);
            UnitEvent.JumpStart
                .RegisterEvent((RoleEntity) Root, ClimbJumpSet)
                .OnlyPlayOnce();
            UnitEvent.JumpEnd
                .RegisterEvent((RoleEntity) Root, ResetHorizontalAndVerticalMoveAble)
                .OnlyPlayOnce();
            handle.Completed += () => UnitEvent.JumpStart.UnRegisterEvent((RoleEntity) Root, ClimbJumpSet);
            handle.Completed += () => UnitEvent.JumpEnd.UnRegisterEvent((RoleEntity) Root, ResetHorizontalAndVerticalMoveAble);
        }
    }
}