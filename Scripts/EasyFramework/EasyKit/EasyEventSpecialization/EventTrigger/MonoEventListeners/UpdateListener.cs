using UnityEngine;

namespace EasyFramework.EventKit
{
    public class UpdateListener: AMonoListener
    {
        private void Update()
        {
            Invoke();
        }
    }
}