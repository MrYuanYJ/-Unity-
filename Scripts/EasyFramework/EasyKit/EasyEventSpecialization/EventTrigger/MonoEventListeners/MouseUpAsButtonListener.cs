using UnityEngine;

namespace EasyFramework
{
    public class MouseUpAsButtonListener: AMonoListener
    {
        private void OnMouseUpAsButton()
        {
            Invoke();
        }
    }
}