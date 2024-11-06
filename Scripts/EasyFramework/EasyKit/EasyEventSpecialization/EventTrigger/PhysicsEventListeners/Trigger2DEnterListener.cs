using System;
using UnityEngine;

namespace EasyFramework
{
    public class Trigger2DEnterListener : AListener2DListener
    {
        private void OnTriggerEnter2D(Collider2D other) => Invoke(other);
    }
}