using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class ExitListenerListener: AListenerListener
    {
        private void OnTriggerExit(Collider other) { Invoke(other); }
    }
}