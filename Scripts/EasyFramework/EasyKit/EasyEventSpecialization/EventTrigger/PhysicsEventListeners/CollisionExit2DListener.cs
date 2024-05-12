using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionExit2DListener: ACollision2DListener
    {
        private void OnCollisionExit2D(Collision2D other) { Invoke(other);  }
    }
}