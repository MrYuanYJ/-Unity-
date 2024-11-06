using UnityEngine;

namespace EasyFramework
{
    public class EnableListener:AMonoListener
    {
        private void OnEnable()
        {
            Invoke();
        }
    }
}