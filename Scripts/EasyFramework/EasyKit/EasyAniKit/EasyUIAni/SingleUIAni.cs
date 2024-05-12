using UnityEngine;

namespace EasyFramework
{
    public class SingleUIAni: ScriptableObject,ISingleAni
    {
        public IEase Ease => ease;

        [SerializeField] private Ease ease;

        public virtual void Playing(float progress, params object[] args){}
    }
}