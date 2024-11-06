using System;
using UnityEngine;

namespace EasyFramework
{
    public class UnitMove2D: AMonoEntityCarrier
    {
        public int moveSpeed;
        public SESProperty<Vector2> InputDirection = new();
        public float smooth=999;
        public bool enableHorizontalMove = true;
        public bool enableVerticalMove = false;
    }
    
    public enum EMovementState
    {
        None,
        Idle,
        Move,
        Fall=100,
        Climb,
        Jump,
    }
    public abstract class MovementState: AEasyState<MovementFSM,RoleEntity>{}
    [Serializable]
    public class MovementFSM: AStateMachine<EMovementState,MovementState,RoleEntity>{}
}