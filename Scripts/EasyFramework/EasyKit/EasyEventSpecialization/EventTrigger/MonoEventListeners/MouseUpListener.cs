using UnityEngine;

namespace EasyFramework
{
    public class MouseUpListener: AMonoListener
    {
        private void OnMouseUp()
        {
            Invoke();
        }
    }
}