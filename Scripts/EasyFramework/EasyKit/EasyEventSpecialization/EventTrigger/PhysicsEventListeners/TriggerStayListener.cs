using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class StayListenerListener : AListenerListener
    {
        private void OnTriggerExit(Collider other) { Invoke(other); }
    }
}