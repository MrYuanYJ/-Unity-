using UnityEngine;

namespace EasyFramework.EventKit
{
    public class EnableListener:AMonoListener
    {
        private void OnEnable()
        {
            Invoke();
        }
    }
}