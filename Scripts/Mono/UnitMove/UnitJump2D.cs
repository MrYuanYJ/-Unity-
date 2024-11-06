using UnityEngine;

namespace EasyFramework
{
    public class UnitJump2D : AMonoEntityCarrier
    {
        public int JumpForce = 10;
        public float maxPressedTime = 0.3f;
        public float gravityMultiplierOnEarlyStopJump = 3f;
        public float coyoteDuration = 0.15f;
        public float coyoteDistance = 0.3f;
        public SESProperty<bool> isJump = new();
        public float jumpHoldTime;
        public Vector2 JumpDirection=Vector2.up;
    }
}
