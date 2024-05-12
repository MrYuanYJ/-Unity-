using System;
using UnityEngine;

namespace EasyFramework.EventKit
{
    public class EnterListenerListener : AListenerListener
    {
        private void OnTriggerEnter(Collider other) { Invoke(other); }
    }
}