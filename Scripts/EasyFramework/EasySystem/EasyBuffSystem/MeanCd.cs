using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public struct MeanCd: IMeanCd<EMeans>
    {
        public float Cd=>cd;
        public EMeans Mean=>mean;

        [SerializeField] private float cd;
        [SerializeField] private EMeans mean;
    }
}