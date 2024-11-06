using System;
using UnityEngine;

namespace EasyFramework
{
    public class CollisionEnterListener: ACollisionListener
    {
        private void OnCollisionEnter(Collision other) { Invoke(other); }
    }
}