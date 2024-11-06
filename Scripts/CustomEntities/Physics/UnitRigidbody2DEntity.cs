

using UnityEngine;

namespace EasyFramework
{
    public class UnitRigidbody2DEntity : AMonoEntity<UnitRigidbody2D>, IFixedUpdateAble
    {
        public override IStructure Structure=>UnitStructure.TryRegister();

        private Vector2 Speed
        {
            get=>Mono.Rigidbody2D.velocity;
            set=>Mono.Rigidbody2D.velocity = value;
        }
        private Vector2 _externalForce;
        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();

        protected override void OnInit()
        {
            Mono.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        protected override void OnActive()
        {
            UnitEvent.SetSpeed2D.RegisterEvent((RoleEntity) Root, SetSpeed);
            UnitEvent.SetSpeed2D_X.RegisterEvent((RoleEntity) Root, SetSpeed_X);
            UnitEvent.SetSpeed2D_Y.RegisterEvent((RoleEntity) Root, SetSpeed_Y);
        }

        protected override void OnUnActive()
        {
            UnitEvent.SetSpeed2D.UnRegisterEvent((RoleEntity) Root, SetSpeed);
            UnitEvent.SetSpeed2D_X.UnRegisterEvent((RoleEntity) Root, SetSpeed_X);
            UnitEvent.SetSpeed2D_Y.UnRegisterEvent((RoleEntity) Root, SetSpeed_Y);
        }

        private void SetSpeed(float x, float y) => Speed = new Vector2(x, y);
        private void SetSpeed_X(float x)=>Speed = new Vector2(x, Speed.y);
        private void SetSpeed_Y(float y)=>Speed = new Vector2(Speed.x, y);
        void IFixedUpdateAble.OnFixedUpdate()
        {
            if (Speed.y < Mono.MaxFallSpeed)
                Speed = new Vector2(Speed.x, Mono.MaxFallSpeed);
            if (_externalForce.magnitude > 0)
                _externalForce = Vector2.Lerp(_externalForce, Vector2.zero, Mathf.MoveTowards(0, 1, 0.5f * EasyTime.FixedDeltaTime));
            Speed += _externalForce;
            UnitEvent.LastFrameSpeed2D.InvokeEvent((RoleEntity)Root, Speed);
        }

    }
}
