namespace EasyFramework
{
    public class IdleState: MovementState
    {
        public static IdleState Instance=new();

        protected override void OnEnter(RoleEntity param) {  }
        protected override void OnExit(RoleEntity param) {  }
    }
}