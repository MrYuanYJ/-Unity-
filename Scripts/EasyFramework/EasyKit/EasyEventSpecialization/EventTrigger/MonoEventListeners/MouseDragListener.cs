using UnityEngine;

namespace EasyFramework
{
    public class MouseDragListener: AMonoListener
    {
        private void OnMouseDrag()
        {
            Invoke();
        }
    }
}