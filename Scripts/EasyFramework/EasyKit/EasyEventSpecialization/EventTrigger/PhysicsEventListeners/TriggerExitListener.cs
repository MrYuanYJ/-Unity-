using System;
using UnityEngine;

namespace EasyFramework
{
    public class TriggerExitListener: AListenerListener
    {
        private void OnTriggerExit(Collider other) { Invoke(other); }
    }
}