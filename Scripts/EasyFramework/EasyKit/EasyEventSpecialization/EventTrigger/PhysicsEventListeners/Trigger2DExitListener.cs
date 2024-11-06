using System;
using UnityEngine;

namespace EasyFramework
{
    public class Trigger2DExitListener : AListener2DListener
    {
        private void OnTriggerExit2D(Collider2D other) => Invoke(other);
    }
}