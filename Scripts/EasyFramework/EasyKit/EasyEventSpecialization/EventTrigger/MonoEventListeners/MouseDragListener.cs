using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseDragListener: AMonoListener
    {
        private void OnMouseDrag()
        {
            Invoke();
        }
    }
}