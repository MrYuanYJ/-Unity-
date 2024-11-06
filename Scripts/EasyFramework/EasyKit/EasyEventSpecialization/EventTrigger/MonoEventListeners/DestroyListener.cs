using UnityEngine;

namespace EasyFramework
{
    public class DestroyListener: AMonoListener
    {
        private void OnDestroy()
        {
            Invoke();
        }
    }
}