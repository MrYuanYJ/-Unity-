namespace EasyFramework
{
    public class UnitClimb2DEntity: AMonoEntity<UnitClimb2D>,IFixedUpdateAble
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public UnitStateCheck2DEntity UnitStateCheck2DEntity;

        protected override void OnInit()
        {
            UnitStateCheck2DEntity = (UnitStateCheck2DEntity) Parent;
        }

        protected override void OnActive()
        {
            UnitEvent.AddMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Climb,ClimbState.Instance);
            UnitFunc.RedirectMovementState.RegisterFunc((RoleEntity) Root, RedirectMovementState);
        }

        protected override void OnUnActive()
        {
            UnitEvent.RemoveMovementState.InvokeEvent((RoleEntity) Root,EMovementState.Climb);
            UnitFunc.RedirectMovementState.UnRegisterFunc((RoleEntity) Root, RedirectMovementState);
        }

        private void Climb()
        {
            var moveDir = UnitFunc.MoveDir2D.InvokeFunc((RoleEntity) Root);
            if (UnitStateCheck2DEntity.Mono.MovementFsm.CurrentState != EMovementState.Climb
                && UnitStateCheck2DEntity.Mono.OnWall)
            {
                UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.Climb);
            }

            if (UnitStateCheck2DEntity.Mono.MovementFsm.CurrentState == EMovementState.Climb)
            {
                if (!UnitStateCheck2DEntity.Mono.OnWall)
                {
                    UnitEvent.ChangeMovementState.InvokeEvent((RoleEntity) Root, EMovementState.None);
                    //ToDo: 发出通知退出爬墙状态 
                }
                UnitEvent.SetSpeed2D_Y.InvokeEvent((RoleEntity) Root,
                    moveDir.y > 0 ? Mono.climbSpeed :
                    moveDir.y == 0 ? Mono.slideSpeed : UnitStateCheck2DEntity.LastFrameSpeed.y);
            }
        }
        private EMovementState RedirectMovementState()
        {
            if (!UnitStateCheck2DEntity.Mono.OnGround)
            {
                if(UnitStateCheck2DEntity.Mono.OnWall)
                    return EMovementState.Climb;
            }
            return EMovementState.None;
        }
        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();
        void IFixedUpdateAble.OnFixedUpdate()
        {
            Climb();
        }
    }
}