using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionExitListener: ACollisionListener
    {
        private void OnCollisionExit(Collision other) { Invoke(other); }
    }
}