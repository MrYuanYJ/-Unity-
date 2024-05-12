using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseUpAsButtonListener: AMonoListener
    {
        private void OnMouseUpAsButton()
        {
            Invoke();
        }
    }
}