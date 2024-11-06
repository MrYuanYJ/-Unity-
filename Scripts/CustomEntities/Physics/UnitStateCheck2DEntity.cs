using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class UnitStateCheck2DEntity: AMonoEntity<UnitStateCheck2D>
    {
        public ContactFilter2D ContactFilter;
        public RaycastHit2D[] RaycastHits = new RaycastHit2D[4];
        public Vector2 DownDirection = Vector2.down;
        public Vector2 GroundNormal;
        public Vector2 WallNormal;
        public Vector2 TopNormal;
        public Vector2 LastFrameSpeed;
        public EasyEvent OnSpeedYDownToZero=new ();

        protected override void OnInit()
        {
            ContactFilter = new ContactFilter2D()
            {
                useTriggers = false,
                layerMask = Mono.groundLayer,
                useLayerMask = true,
            };
        }

        protected override void OnActive()
        {
            Mono.MovementFsm.AddState(EMovementState.Idle, IdleState.Instance);
            Mono.MovementFsm.AddState(EMovementState.Fall, FallState.Instance);
    
            UnitEvent.LastFrameSpeed2D.RegisterEvent((RoleEntity)Root, SetLastFrameSpeed);
            UnitEvent.AddMovementState.RegisterEvent((RoleEntity)Root, AddState);
            UnitEvent.RemoveMovementState.RegisterEvent((RoleEntity)Root, RemoveState);
            UnitEvent.ChangeMovementState.RegisterEvent((RoleEntity)Root, ChangeMovementState);
            UnitFunc.CurrentMovementState.RegisterFunc((RoleEntity)Root, GetCurrentMovementState);
            UnitFunc.RedirectMovementState.RegisterFunc((RoleEntity)Root, RedirectMovementState);
        }

        protected override void OnUnActive()
        {
            Mono.MovementFsm.RemoveState(EMovementState.Idle);
            Mono.MovementFsm.RemoveState(EMovementState.Fall);
            
            UnitEvent.LastFrameSpeed2D.UnRegisterEvent((RoleEntity)Root, SetLastFrameSpeed);
            UnitEvent.AddMovementState.UnRegisterEvent((RoleEntity)Root, AddState);
            UnitEvent.RemoveMovementState.UnRegisterEvent((RoleEntity)Root, RemoveState);
            UnitEvent.ChangeMovementState.UnRegisterEvent((RoleEntity)Root, ChangeMovementState);
            UnitFunc.CurrentMovementState.UnRegisterFunc((RoleEntity)Root, GetCurrentMovementState);
            UnitFunc.RedirectMovementState.UnRegisterFunc((RoleEntity)Root, RedirectMovementState);
        }

        private void AddState(EMovementState state, MovementState stateInstance)
        {
            Mono.MovementFsm.AddState(state, stateInstance);
        }
        
        private void RemoveState(EMovementState state)
        {
            Mono.MovementFsm.RemoveState(state);
        }
        private void ChangeMovementState(EMovementState state)
        {
            if (state != EMovementState.None)
                Mono.MovementFsm.ChangeState(state, (RoleEntity) Root);
            else
            {
                UnitFunc.RedirectMovementState.InvokeAndReturnAll((RoleEntity) Root).ForReverse((i, state) =>
                {
                    if (state != EMovementState.None)
                    {
                        Mono.MovementFsm.ChangeState(state, (RoleEntity) Root);
                        return false;
                    }
                    return true;
                });
            }
        }
        private EMovementState RedirectMovementState()
        {
            if (Mono.MovementFsm.CurrentState != EMovementState.Fall
                && !Mono.OnGround)
                return EMovementState.Fall;
            if(Mono.OnGround
               && Mathf.Abs(LastFrameSpeed.x)<0.05f)
                return EMovementState.Idle;
            return EMovementState.None;
        }
        private EMovementState GetCurrentMovementState()=> Mono.MovementFsm.CurrentState;

        private void SetLastFrameSpeed(Vector2 speed)
        {
            if (!Mono.OnGround)
            {
                if (Mono.MovementFsm.CurrentState != EMovementState.Fall
                    && (int)Mono.MovementFsm.CurrentState < 100
                    && speed.y < 0)
                    ChangeMovementState(EMovementState.Fall);
            }
            else if (Mono.MovementFsm.CurrentState == EMovementState.Fall)
                ChangeMovementState(EMovementState.None);

            if (LastFrameSpeed.y > 0 && speed.y <= 0)
            {
                OnSpeedYDownToZero?.Invoke();
            }
            LastFrameSpeed = speed;
            CheckLayer();
        }

        private void CheckLayer()
        {
            var hitCount = Mono.CapsuleCollider2D.Cast(LastFrameSpeed.normalized, ContactFilter, RaycastHits,
                LastFrameSpeed.magnitude * EasyTime.FixedDeltaTime);

            if (hitCount == 0 && LastFrameSpeed == Vector2.zero) return;
            if (Mathf.Abs(LastFrameSpeed.y) > 0.05f)
            {
                Mono.OnGround = false;
                Mono.OnTop = false;
                GroundNormal = Vector2.zero;
                TopNormal = Vector2.zero;
            }

            if (Mathf.Abs(LastFrameSpeed.x) > 0.05f)
            {
                Mono.OnWall = false;
                Mono.OnLeftWall = false;
                Mono.OnRightWall = false;
                WallNormal = Vector2.zero;
            }

            for (int i = 0; i < hitCount; i++)
            {
                var hit = RaycastHits[i];
                if (hit.collider != null)
                {
                    if (hit.normal.y >= Mathf.Abs(hit.normal.x))
                    {
                        Mono.OnGround = true;
                        GroundNormal = hit.normal;
                    }
                    else if (Mathf.Abs(hit.normal.y) < Mathf.Abs(hit.normal.x))
                    {
                        Mono.OnWall = true;
                        if (hit.normal.x > 0)
                        {
                            Mono.OnLeftWall = true;
                            WallNormal = hit.normal;
                        }
                        else if (hit.normal.x < 0)
                        {
                            Mono.OnRightWall = true;
                            WallNormal = hit.normal;
                        }
                    }
                    else
                    {
                        Mono.OnTop = true;
                        TopNormal = hit.normal;
                    }
                }
            }

            if (!Mono.OnGround)
            {
                hitCount = Mono.CapsuleCollider2D.Cast(DownDirection, ContactFilter, RaycastHits,
                    Mono.MaxGroundCheckDistance);
                Mono.Height = Mono.MaxGroundCheckDistance;
                for (int i = 0; i < hitCount; i++)
                {
                    var hit = RaycastHits[i];
                    if (hit.normal.y >= Mathf.Abs(hit.normal.x))
                    {
                        if (hit.distance < 0.05f)
                        {
                            Mono.OnGround = true;
                            Mono.Height = 0;
                        }
                        else
                            Mono.Height = hit.distance;

                        GroundNormal = hit.normal;
                        break;
                    }
                }
            }
            else
                Mono.Height = 0;
        }

        public override IStructure Structure => UnitStructure.TryRegister();
    }
}