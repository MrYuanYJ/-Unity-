using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseUpListener: AMonoListener
    {
        private void OnMouseUp()
        {
            Invoke();
        }
    }
}