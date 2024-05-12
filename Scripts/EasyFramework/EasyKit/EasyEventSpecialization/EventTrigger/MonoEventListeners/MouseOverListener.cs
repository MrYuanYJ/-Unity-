using UnityEngine;

namespace EasyFramework.EventKit
{
    public class MouseOverListener: AMonoListener
    {
        private void OnMouseOver()
        {
            Invoke();
        }
    }
}