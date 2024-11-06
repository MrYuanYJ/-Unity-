using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework.EasySystem
{
    [System.Serializable]
    public class AudioSystemSetting: AScriptableObjectSingleton<AudioSystemSetting>
    {
        [SerializeField]private List<RandomPathDataView> randomPathDatas = new ();
        public static Dictionary<string, RandomPathData> RandomPathDataDic = new();
        public static Dictionary<EAudioTrack, AudioSystemData> AudioSystemDataDic = new();
        public static bool Mute = false;


        public void Init()
        {
            foreach (var data in randomPathDatas)
            {
                RandomPathDataDic.Add(data.mainPath, new RandomPathData()
                {
                    mainPath = data.mainPath,
                    additionalInformation= new List<string>(data.additionalInformation)
                });
            }
        }
        public static AudioSystemData GetAudioSystemData(EAudioTrack track)
        {
            if (!AudioSystemDataDic.TryGetValue(track,out var data))
            {
                data = new AudioSystemData(false, 1.0f);
                AudioSystemDataDic.Add(track, data);
            }
            return data;
        }

        public static AudioSystemData SetAudioSystemData(EAudioTrack track, bool isMute, float volume)
        {
            var data = new AudioSystemData(isMute, volume);
            AudioSystemDataDic[track] = data;
            
            return data;
        }

        public static void SetTrackMute(EAudioTrack track,bool mute)
        {
            var data = GetAudioSystemData(track);
            SetAudioSystemData(track, mute, data.Volume);
        }

        public static void SetTrackVolume(EAudioTrack track, float volume)
        {
            var data = GetAudioSystemData(track);
            SetAudioSystemData(track, data.Mute, volume);
        }
    }
}