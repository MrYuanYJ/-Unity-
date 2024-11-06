using UnityEngine;

namespace EasyFramework.EasySystem
{
    [System.Serializable]
    public struct AudioSystemData
    {
        internal bool Mute;
        [Range(0f,1f)]
        public float Volume;

        public AudioSystemData(bool mute, float volume)
        {
            Mute = mute;
            Volume = volume;
        }
        public bool IsMute=> Mute && AudioSystemSetting.Mute;
    }
}