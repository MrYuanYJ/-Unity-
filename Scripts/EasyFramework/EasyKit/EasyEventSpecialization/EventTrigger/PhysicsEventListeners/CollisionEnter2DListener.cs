using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionEnter2DListener: ACollision2DListener
    {
        private void OnCollisionEnter2D(Collision2D other) { Invoke(other);  }
    }
}