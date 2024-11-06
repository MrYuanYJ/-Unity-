using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class UnitJump2DEntity : AMonoEntity<UnitJump2D>,IFixedUpdateAble
    {
        public override IStructure Structure=>UnitStructure.TryRegister();
        
        public UnitStateCheck2DEntity UnitStateCheck2DEntity;
        public RoleProperties JumpForce;
        public CoroutineHandle CoyoteTimerHandle;
        

        protected override void OnInit()
        {
            UnitStateCheck2DEntity = (UnitStateCheck2DEntity)Parent;
            JumpForce = BBPropertyFunc.SetRoleProperty((RoleEntity)Root,EUnitProperty.JumpForce.ToString(),new RoleProperties(Mono.JumpForce));
            //JumpForce = UnitFunc.UnitProperty.InvokeFunc((UnitEntity)Root,EUnitProperty.JumpForce);
        }

        protected override void OnActive()
        {
            UnitEvent.AddMovementState.InvokeEvent((RoleEntity)Root,EMovementState.Jump,JumpState.Instance);
            UnitEvent.SetJumpDir2D.RegisterEvent((RoleEntity) Root, SetJumpDirection);
            UnitFunc.JumpCoyoteTimer.RegisterFunc((RoleEntity) Root,CoyoteTimer);
            UnitFunc.JumpCoyoteTimer.RegisterFunc((RoleEntity) Root,CustomCoyoteTimer);
            Mono.isJump.Register(CheckJump);
        }

        protected override void OnUnActive()
        {
            UnitEvent.RemoveMovementState.InvokeEvent((RoleEntity)Root,EMovementState.Jump);
            UnitEvent.SetJumpDir2D.UnRegisterEvent((RoleEntity) Root, SetJumpDirection);
            UnitFunc.JumpCoyoteTimer.UnRegisterFunc((RoleEntity) Root,CoyoteTimer);
            UnitFunc.JumpCoyoteTimer.UnRegisterFunc((RoleEntity) Root,CustomCoyoteTimer);
            Mono.isJump.UnRegister(CheckJump);
        }

        private void CheckJump(bool isPressed)
        {
            if (isPressed && UnitFunc.CurrentMovementState.InvokeFunc((RoleEntity) Root) != EMovementState.Jump)
            {
                if (UnitStateCheck2DEntity.Mono.OnGround
                    || UnitFunc.JumpCondition.InvokeAndReturnAll((RoleEntity) Root).Get(canJump=>canJump)
                    || CoyoteTimerHandle!=null)
                    JumpStart();
                else
                    Mono.isJump.SetSilently(false);
            }
            else if (!isPressed)
                JumpEnd();
        }

        private void JumpStart()
        {
            if (UnitStateCheck2DEntity.Mono.MovementFsm.CurrentState < EMovementState.Jump)
            {
                UnitEvent.JumpStart.InvokeEvent((RoleEntity) Root);
                if(CoyoteTimerHandle!=null)
                    CoyoteTimerHandle.Cancel();
                var jumpSpeed = JumpForce.Value * Mono.JumpDirection.normalized;
                Mono.JumpDirection= Vector2.up;
                UnitEvent.SetSpeed2D.InvokeEvent((RoleEntity) Root,
                        arg1: jumpSpeed.x,
                        arg2: Mathf.Max(UnitStateCheck2DEntity.LastFrameSpeed.y + jumpSpeed.y, jumpSpeed.y));
                UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.Jump);
            }
        }

        private void JumpHold()
        {      
            if (Mono.isJump.Value)
            {
                Mono.jumpHoldTime += EasyTime.FixedDeltaTime;
                if (Mono.jumpHoldTime >= Mono.maxPressedTime)
                {
                    JumpEnd();
                }
            }
        }

        private void JumpEnd()
        {
            Mono.isJump.SetSilently(false);
            if (UnitStateCheck2DEntity.LastFrameSpeed.y > 0 && Mono.jumpHoldTime < Mono.maxPressedTime)
            {
                FixedUpdateEvent.Register(SpeedDownOnEarlyStopJump);
                UnitStateCheck2DEntity.OnSpeedYDownToZero
                    .Register(() => FixedUpdateEvent.UnRegister(SpeedDownOnEarlyStopJump)).OnlyPlayOnce();
            }
            Mono.jumpHoldTime = 0;
            UnitEvent.JumpEnd.InvokeEvent((RoleEntity) Root);
            UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.None);
        }
        private CoroutineHandle CoyoteTimer()=>CustomCoyoteTimer(Mono.coyoteDuration);
        private CoroutineHandle CustomCoyoteTimer(float coyoteTime)
        {
            CoyoteTimerHandle?.Cancel();
            CoyoteTimerHandle = EasyTask.Delay(coyoteTime);
            CoyoteTimerHandle.Cancelled += () => CoyoteTimerHandle = null;
            CoyoteTimerHandle.Completed += () => CoyoteTimerHandle = null;
            return CoyoteTimerHandle;
        }
        private void SpeedDownOnEarlyStopJump()
        {
            UnitEvent.SetSpeed2D_Y.InvokeEvent((RoleEntity) Root,
                UnitStateCheck2DEntity.LastFrameSpeed.y + Mono.gravityMultiplierOnEarlyStopJump * Physics2D.gravity.y *
                EasyTime.FixedDeltaTime);
        }

        private void SetJumpDirection(Vector2 direction)
        {
            Mono.JumpDirection = direction;
        }

        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();

        void IFixedUpdateAble.OnFixedUpdate()
        {
            JumpHold();
            if (CoyoteTimerHandle == null
                && UnitStateCheck2DEntity.Mono.Height < Mono.coyoteDistance
                && UnitStateCheck2DEntity.Mono.MovementFsm.CurrentState == EMovementState.Fall)
                CoyoteTimer();
        }
    }
}
