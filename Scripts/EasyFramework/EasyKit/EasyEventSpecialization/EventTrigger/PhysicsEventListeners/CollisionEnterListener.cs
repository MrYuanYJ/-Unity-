using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class CollisionEnterListener: ACollisionListener
    {
        private void OnCollisionEnter(Collision other) { Invoke(other); }
    }
}