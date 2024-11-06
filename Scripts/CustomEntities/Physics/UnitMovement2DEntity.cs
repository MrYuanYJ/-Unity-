using UnityEngine;

namespace EasyFramework
{
    public class UnitMovement2DEntity: AMonoEntity<UnitMove2D>,IFixedUpdateAble
    {
        public override IStructure Structure => UnitStructure.TryRegister();

        public Vector2 Velocity => SpeedProperty.Value * Mono.InputDirection.Value;
        public RoleProperties SpeedProperty ;
        public UnitStateCheck2DEntity UnitStateCheck2DEntity;
        private float smooth = 2f;

        protected override void OnInit()
        {
            UnitStateCheck2DEntity = (UnitStateCheck2DEntity)Parent;
            SpeedProperty = BBPropertyFunc.SetRoleProperty((RoleEntity) Root, EUnitProperty.MoveSpeed.ToString(), new RoleProperties(Mono.moveSpeed,0));
            //SpeedProperty = UnitFunc.UnitProperty.InvokeFunc((UnitEntity)Root,EUnitProperty.MoveSpeed);
            var enableHorizontalMove= Mono.enableHorizontalMove;
            var enableVerticalMove= Mono.enableVerticalMove;
            UnitEvent.ResetHorizontalAndVerticalMoveAble.RegisterEvent((RoleEntity) Root, () =>
            {
                Mono.enableHorizontalMove = enableHorizontalMove;
                Mono.enableVerticalMove = enableVerticalMove;
            }).UnRegisterOnDispose((RoleEntity) Root);
            UnitEvent.BanMove.RegisterEvent((RoleEntity) Root, BanMove);
        }

        protected override void OnActive()
        {
            Mono.InputDirection.Register(MovementStateChange);
            UnitEvent.AddMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Move, MoveState.Instance);
            UnitEvent.AddMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Climb, ClimbState.Instance);
            UnitEvent.SetMoveDir2D.RegisterEvent((RoleEntity) Root, Mono.InputDirection.Set);
            UnitFunc.RedirectMovementState.RegisterFunc((RoleEntity) Root, RedirectMovementState);
            UnitFunc.MoveDir2D.RegisterFunc((RoleEntity) Root,MoveDir2D);
        }
        
        protected override void OnUnActive()
        {
            Mono.InputDirection.UnRegister(MovementStateChange);
            UnitEvent.RemoveMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Move);
            UnitEvent.RemoveMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Climb);
            UnitEvent.SetMoveDir2D.UnRegisterEvent((RoleEntity) Root, Mono.InputDirection.Set);
            UnitFunc.RedirectMovementState.RegisterFunc((RoleEntity) Root, RedirectMovementState);
            UnitFunc.MoveDir2D.UnRegisterFunc((RoleEntity) Root,MoveDir2D);;
        }


        protected override void OnDispose(bool usePool)
        {
            SpeedProperty = null;
            UnitEvent.BanMove.UnRegisterEvent((RoleEntity) Root, BanMove);
        }

        private Vector2 MoveDir2D() => Mono.InputDirection.Value;

        private void BanMove()
        {
            Mono.enableHorizontalMove = false;
            Mono.enableVerticalMove = false;
        }

        private void Move()
        {
            if (Mono.InputDirection.Value != Vector2.zero)
            {
                SpeedProperty.AddValuePercent(Mono.smooth * EasyTime.FixedDeltaTime);
            }
            else
                SpeedProperty.AddValuePercent(-Mono.smooth * EasyTime.FixedDeltaTime);

            if (Mono.enableHorizontalMove)
                UnitEvent.SetSpeed2D_X.InvokeEvent((RoleEntity) Root,
                    Velocity.x == 0 || Mono.InputDirection.Value.x * UnitStateCheck2DEntity.WallNormal.x < 0
                        ? Mathf.Lerp(UnitStateCheck2DEntity.LastFrameSpeed.x, 0, Mathf.MoveTowards(0, 1, smooth * EasyTime.FixedDeltaTime))
                        : Velocity.x>0
                            ?Mathf.Max(Velocity.x, UnitStateCheck2DEntity.LastFrameSpeed.x)
                            :Mathf.Min(Velocity.x, UnitStateCheck2DEntity.LastFrameSpeed.x));

            if (Mono.enableVerticalMove && Velocity.y != 0)
                UnitEvent.SetSpeed2D_Y.InvokeEvent((RoleEntity) Root,
                    Velocity.y > 0
                        ? Mathf.Max(Velocity.y, UnitStateCheck2DEntity.LastFrameSpeed.y)
                        : Mathf.Min(Velocity.y, UnitStateCheck2DEntity.LastFrameSpeed.y));
        }

        private void MovementStateChange(Vector2 value)
        {
            if ((int)UnitStateCheck2DEntity.Mono.MovementFsm.CurrentState<100)
            {
                if (value != Vector2.zero)
                {
                    if (value.x != 0 && Mono.enableHorizontalMove || value.y != 0 && Mono.enableVerticalMove)
                        UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.Move);
                }
                else if (UnitStateCheck2DEntity.Mono.OnGround)
                {
                    if (value == Vector2.zero)
                        UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.Idle);
                }
                else
                {
                    if (value == Vector2.zero)
                        UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.None);
                }
            }
        }
        private EMovementState RedirectMovementState()
        {
            if (UnitStateCheck2DEntity.Mono.OnGround)
            {
                if (Mono.InputDirection.Value != Vector2.zero)
                    return EMovementState.Move;
                else
                    return EMovementState.Idle;
            }
            return EMovementState.None;
        }
        
        public IEasyEvent FixedUpdateEvent { get; set; }=new EasyEvent();

        void IFixedUpdateAble.OnFixedUpdate()
        {
            Move();
        }
    }
}