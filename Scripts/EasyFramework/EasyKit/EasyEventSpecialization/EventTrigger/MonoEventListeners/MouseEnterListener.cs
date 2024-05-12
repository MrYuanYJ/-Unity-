using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseEnterListener:AMonoListener
    {
        private void OnMouseEnter()
        {
            Invoke();
        }
    }
}