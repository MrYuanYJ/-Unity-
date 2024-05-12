using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class Listener2DExitListener : AListener2DListener
    {
        private void OnTriggerExit2D(Collider2D other) => Invoke(other);
    }
}