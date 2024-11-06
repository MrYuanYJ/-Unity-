using System;
using UnityEngine;

namespace EasyFramework
{
    public class Trigger2DStayListener : AListener2DListener
    {
        private void OnTriggerStay2D(Collider2D other) => Invoke(other);
    }
}