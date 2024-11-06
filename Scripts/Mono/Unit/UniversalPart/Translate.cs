using UnityEngine;

namespace EasyFramework
{
    [AddComponentMenu("Easy Framework/Universal/Translate")]
    public class Translate: AMonoEntityCarrier
    {
        public Transform translateTarget;
        public float singleCycleDuration = 1.0f;
    }
}