using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionStay2DListener: ACollision2DListener
    {
        private void OnCollisionStay2D(Collision2D other) { Invoke(other);  }
    }
}