using UnityEngine;

namespace EasyFramework.EventKit
{
    public class DisableListener: AMonoListener
    {
        private void OnDisable()
        {
            Invoke();
        }
    }
}