using System;
using UnityEngine;

namespace EasyFramework
{
    public class CollisionStayListener: ACollisionListener
    {
        private void OnCollisionStay(Collision other) { Invoke(other); }
    }
}