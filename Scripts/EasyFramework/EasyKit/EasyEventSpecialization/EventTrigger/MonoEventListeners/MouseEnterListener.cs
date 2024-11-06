using UnityEngine;

namespace EasyFramework
{
    public class MouseEnterListener:AMonoListener
    {
        private void OnMouseEnter()
        {
            Invoke();
        }
    }
}