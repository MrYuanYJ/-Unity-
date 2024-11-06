using UnityEngine;

namespace EasyFramework
{
    public class FixedUpdateListener:AMonoListener
    {
        private void FixedUpdate()
        {
            Invoke();
        }
    }
}