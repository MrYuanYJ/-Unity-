using UnityEngine;
using UnityEngine.Events;

namespace EasyFramework
{
    [System.Serializable]
    public class SingleAni: ISingleAni
    {
        public IEase Ease => ease;

        [SerializeField]private Ease ease;
        [SerializeField]private UnityEvent<float> playing;

        void ISingleAni.Playing(float progress,params object[] args)
        {
            playing?.Invoke(progress);
        }
    }
}