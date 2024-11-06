using System;
using UnityEngine;

namespace EasyFramework
{
    public class CollisionExitListener: ACollisionListener
    {
        private void OnCollisionExit(Collision other) { Invoke(other); }
    }
}