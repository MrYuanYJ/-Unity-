using System;
using UnityEngine;

namespace EasyFramework
{
    public class TriggerEnterListener : AListenerListener
    {
        private void OnTriggerEnter(Collider other) { Invoke(other); }
    }
}