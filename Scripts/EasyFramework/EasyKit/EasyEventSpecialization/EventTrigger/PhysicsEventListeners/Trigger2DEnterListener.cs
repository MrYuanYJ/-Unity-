using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class Listener2DEnterListener : AListener2DListener
    {
        private void OnTriggerEnter2D(Collider2D other) => Invoke(other);
    }
}