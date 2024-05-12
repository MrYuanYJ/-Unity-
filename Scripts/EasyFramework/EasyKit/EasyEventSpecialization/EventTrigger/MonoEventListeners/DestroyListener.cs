using UnityEngine;

namespace EasyFramework.EventKit
{
    public class DestroyListener: AMonoListener
    {
        private void OnDestroy()
        {
            Invoke();
        }
    }
}