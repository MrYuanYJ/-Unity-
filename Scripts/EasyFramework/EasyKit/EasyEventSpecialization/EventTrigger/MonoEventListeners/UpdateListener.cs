using UnityEngine;

namespace EasyFramework
{
    public class UpdateListener: AMonoListener
    {
        private void Update()
        {
            Invoke();
        }
    }
}