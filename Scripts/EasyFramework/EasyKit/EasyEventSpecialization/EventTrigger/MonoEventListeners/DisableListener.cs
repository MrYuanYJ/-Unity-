using UnityEngine;

namespace EasyFramework
{
    public class DisableListener: AMonoListener
    {
        private void OnDisable()
        {
            Invoke();
        }
    }
}