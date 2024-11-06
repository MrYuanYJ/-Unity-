using System;
using Sirenix.Utilities;
using UnityEngine;

namespace EasyFramework.EasySystem
{
    [System.Serializable]
    public struct AudioParam
    {
        [DropDown]
        public EAudioTrack audioTrack;
        [DropDown]
        public EAudioPlayMode playMode;
        public string audio;
        public float startTime;
        public bool loop;
        [Range(0, 256)] public int priority;
        [Range(0f, 1f)] public float volume;
        [Range(0f, 1f)] public float pitch;
        [Range(-1f, 1f)] public float stereoPan;
        [Range(0f, 1f)] public float spatialBlend;
        [Range(0f, 1.1f)] public float reverbZoneMix;
        [Range(0f, 5f)] public float dopplerLevel;
        [Range(0, 360)] public float spread;
        public AudioRolloffMode rolloffMode;
        public float minDistance;
        public float maxDistance;
        public CoroutineHandle VolumeCoroutineHandle;

        public override bool Equals(object obj)
        {
            if(obj is AudioParam other)
                return Equals(other);
            return false;
        }

        public bool Equals(AudioParam other)
        {
            return audioTrack == other.audioTrack
                   && audio == other.audio
                   && playMode == other.playMode;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(audioTrack);
            hashCode.Add(audio);
            hashCode.Add(playMode);
            return hashCode.ToHashCode();
        }

        public static bool operator ==(AudioParam left, AudioParam right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(AudioParam left, AudioParam right)
        {
            return !(left == right);
        }

        public static AudioParam Default2D(EAudioTrack audioTrack,string audioClip,bool loop=false,EAudioPlayMode playMode=EAudioPlayMode.Default)
        {
            return new AudioParam()
            {
                audioTrack = audioTrack,
                playMode = playMode,
                audio = audioClip,
                loop = loop,
                priority = 128,
                volume = 1f,
                pitch = 1f,
                stereoPan = 0f,
                spatialBlend = 0f,
                reverbZoneMix = 1f
            };
        }
        public static AudioParam Default3D(EAudioTrack audioTrack,string audioClip, bool loop = false,EAudioPlayMode playMode=EAudioPlayMode.Default)
        {
            return new AudioParam()
            {
                audioTrack = audioTrack,
                playMode = playMode,
                audio = audioClip,
                loop = loop,
                priority = 128,
                volume = 1f,
                pitch = 1f,
                stereoPan = 0f,
                spatialBlend = 0.95f,
                reverbZoneMix = 1f,
                dopplerLevel = 1f,
                spread = 0f,
                rolloffMode = AudioRolloffMode.Logarithmic,
                minDistance = 1f,
                maxDistance = 50f
            };
        }
    }
}