using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseDownListener: AMonoListener
    {
        private void OnMouseDown()
        {
            Invoke();
        }
    }
}