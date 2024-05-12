using UnityEngine;

namespace EasyFramework.EventKit
{
    public class FixedUpdateListener:AMonoListener
    {
        private void FixedUpdate()
        {
            Invoke();
        }
    }
}