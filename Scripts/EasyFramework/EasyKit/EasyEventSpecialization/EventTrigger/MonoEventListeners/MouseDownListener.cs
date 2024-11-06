using UnityEngine;

namespace EasyFramework
{
    public class MouseDownListener: AMonoListener
    {
        private void OnMouseDown()
        {
            Invoke();
        }
    }
}