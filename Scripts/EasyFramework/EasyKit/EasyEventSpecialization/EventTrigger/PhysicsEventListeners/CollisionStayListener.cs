using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionStayListener: ACollisionListener
    {
        private void OnCollisionStay(Collision other) { Invoke(other); }
    }
}