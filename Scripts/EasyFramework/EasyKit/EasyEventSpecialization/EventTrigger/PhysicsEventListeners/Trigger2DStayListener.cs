using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class Listener2DStayListener : AListener2DListener
    {
        private void OnTriggerStay2D(Collider2D other) => Invoke(other);
    }
}