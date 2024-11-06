using UnityEngine;

namespace EasyFramework
{
    public class MouseOverListener: AMonoListener
    {
        private void OnMouseOver()
        {
            Invoke();
        }
    }
}