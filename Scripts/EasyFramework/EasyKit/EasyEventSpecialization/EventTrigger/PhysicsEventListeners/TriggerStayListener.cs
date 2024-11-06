using System;
using UnityEngine;

namespace EasyFramework
{
    public class TriggerStayListener : AListenerListener
    {
        private void OnTriggerExit(Collider other) { Invoke(other); }
    }
}