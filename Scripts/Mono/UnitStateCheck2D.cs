using UnityEngine;

namespace EasyFramework
{
    public class UnitStateCheck2D: AMonoEntityCarrier
    {
        public CapsuleCollider2D CapsuleCollider2D;
        public float MaxGroundCheckDistance = 30f;
        public LayerMask groundLayer;
        public bool OnGround = true;
        public bool OnTop;
        public bool OnWall;
        public bool OnLeftWall;
        public bool OnRightWall;
        public float Height;
        public MovementFSM MovementFsm = new();
    }
}