using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework.EasySystem
{
    public enum EAudioTrack
    {
        Default,
        Voice,
        Effect,
        
        Ui,
        Ambient,
        Music,
    }

    public enum EAudioPlayMode
    {
        Default,
        Replay,
        PlayOneShot,
    }

    public class AudioClipRefCount
    {
        public AudioClip AudioClip;
        public int RefCount;
    }
    public class EasyAudioSystem : ASystem,IUpdateAble
    {
        private static AudioSource FetchAudioSource()
        {
            var audioSource = (AudioSource)EasyRes.FetchComponent.InvokeFunc(typeof(AudioSource));
            if (!audioSource)
            {
                var audioSourceGo = new GameObject("AudioSource");
                audioSource = audioSourceGo.AddComponent<AudioSource>();
                audioSource.gameObject.Register<DisableListener>(Cancel);
                audioSource.gameObject.RegisterOnDestroy(()=>
                {
                    Cancel();
                    AudioSourceParamDic.Remove(audioSource);
                });
            }
            
            audioSource.gameObject.SetActive(true);
            return audioSource;

            void Cancel()
            {
                if(AudioSourceParamDic.TryGetValue(audioSource,out var data))
                    data.AudioParam.VolumeCoroutineHandle.Cancel();
            }
        }

        private static readonly Dictionary<AudioParam, AudioSource> AudioSource2DDic = new();
        private static readonly Dictionary<GameObject, Dictionary<AudioParam, AudioSource>> AudioSource3DDic = new();
        private static readonly Dictionary<AudioSource, AudioSourceData> AudioSourceParamDic = new();
        private static readonly Dictionary<string,AudioClipRefCount> AudioClipRefCountDic = new();
        private static CoroutineHandle _audioClipRefCountTimerHandle;
        private static AudioListener _audioListener;
        
        internal static void SetAudioSourceData(AudioSource audioSource, AudioSourceData audioSourceData)
        {
            AudioSourceParamDic[audioSource] = audioSourceData;
        }
        
        public static AudioSourceData GetAudioSourceData(AudioSource audioSource)
        {
            return AudioSourceParamDic[audioSource];
        }
        public static AudioParam GetAudioParam(AudioSource audioSource)
        {
            return AudioSourceParamDic[audioSource].AudioParam;
        }

        public static AudioListener AudioListener()
        {
            if (_audioListener)
                return _audioListener;
            var audioListener = Object.FindObjectOfType<AudioListener>();
            var audioListenerGo = new GameObject("AudioListener");
            if (audioListener)
            {
                audioListenerGo.transform.SetParent(audioListener.transform);
                Object.Destroy(audioListener);
            }
            _audioListener = audioListenerGo.AddComponent<AudioListener>();
            return _audioListener;
        }
        public static void AudioListener(Transform transform)
        {
            AudioListener();
            var audioListenerTrans = _audioListener.transform;
            if (audioListenerTrans.parent == transform)
                return;
            audioListenerTrans.SetParent(transform);
            audioListenerTrans.localPosition = Vector3.zero;
            audioListenerTrans.localScale = Vector3.one;
        }

        private AudioSource FetchAudioSourceByAudioParam(AudioParam audioParam,Dictionary<AudioParam, AudioSource> allAudioSourceDic)
        {
            if (!allAudioSourceDic.TryGetValue(audioParam, out var audioSource))
            {
                audioSource = FetchAudioSource();
                allAudioSourceDic[audioParam] = audioSource;
                SetAudioSourceData(audioSource,new AudioSourceData()
                {
                    AudioParam = audioParam,
                    Volume = audioParam.volume,
                    NeedRefresh = false
                });
            }

            audioSource.SetParam(audioParam);
            return audioSource;
        }

        private void WaitEndOfFrameRemoveAudioSourceFromDic(AudioParam audioParam, Dictionary<AudioParam, AudioSource> allAudioSourceDic)
        {
            EasyCoroutine.WaitEndOfFrame(() => allAudioSourceDic.Remove(audioParam));
        }
        private AudioSource FetchAudioSource3DByAudioParam(GameObject go,AudioParam audioParam)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var audioSourceDic))
            {
                audioSourceDic = new Dictionary<AudioParam, AudioSource>();
                AudioSource3DDic.Add(go, audioSourceDic);
            }
            AudioSource audioSource= FetchAudioSourceByAudioParam(audioParam,audioSourceDic);

            var audioSourceTrans= audioSource.transform;
            audioSourceTrans.SetParent(go.transform);
            audioSourceTrans.localPosition = Vector3.zero;
            audioSourceTrans.localScale = Vector3.one;
            return audioSource;
        }


        private bool GetAudioSource(AudioParam audioParam, Dictionary<AudioParam, AudioSource> allAudioSourceDic,out AudioSource audioSource)
        {
            return allAudioSourceDic.TryGetValue(audioParam, out audioSource);
        }
        public bool GetAudioSource2D(AudioParam audioParam, out AudioSource audioSource) => GetAudioSource(audioParam, AudioSource2DDic, out audioSource);
        public bool GetAudioSource3D(GameObject go,AudioParam audioParam, out AudioSource audioSource)
        {
            if (AudioSource3DDic.TryGetValue(go, out var audioSourceDic))
                return GetAudioSource(audioParam, audioSourceDic, out audioSource);
            
            audioSource = null;
            return false;
        }


        private CoroutineHandle<AudioClip> GetAudioClip(string audioName)
        {
            var audioClipPath = GetAudioClipPath(audioName);
            if (AudioClipRefCountDic.TryGetValue(audioClipPath, out var refCount))
            {
                refCount.RefCount = 3;
                return CoroutineHandle<AudioClip>.Fetch(refCount.AudioClip);
            }
            var handle=(CoroutineHandle<Object>)EasyRes.LoadAssetAsync.InvokeFunc(typeof(AudioClip), audioClipPath, false);
            handle.OnCompleted(() => AudioClipRefCountDic.Add(audioClipPath,
                new AudioClipRefCount() {AudioClip = handle.Result as AudioClip, RefCount = 3}));
            return EasyCoroutine.Await(handle, obj => obj as AudioClip);
        }
        /// <summary>根据音频名称获取音频路径(一个音频名称可能对应多个随机的音频文件)</summary>
        private string GetAudioClipPath(string audioName)
        {
            if (AudioSystemSetting.RandomPathDataDic.TryGetValue(audioName, out var randomPathData))
            {
                return randomPathData.GetRandomPath();
            }
            
            return audioName;
        }
        
        public void SetTrackMute(EAudioTrack track,bool mute)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track)
                    item.Value.mute = mute;
            }
            

            foreach (var audioSourceDic in AudioSource3DDic.Values)
            {
                foreach (var item in audioSourceDic)
                {
                    if (item.Key.audioTrack == track)
                        item.Value.mute = mute;
                }
            }
            AudioSystemSetting.SetTrackMute(track,mute);
        }
        public void SetTrackVolume(EAudioTrack track, float volume)
        {
            AudioSystemSetting.SetTrackVolume(track,volume);
            foreach (var item in AudioSourceParamDic)
            {
                if (item.Value.AudioParam.audioTrack == track)
                    item.Key.RequestRefreshAudioSource();
            }
        }
        public void SetMute(bool mute)
        {
            if(AudioSystemSetting.Mute==mute)
                return;
            foreach (var item in AudioSource2DDic)
                item.Value.mute = mute;
            
            foreach (var audioSourceDic in AudioSource3DDic.Values)
                foreach (var item in audioSourceDic)
                    item.Value.mute = mute;

            AudioSystemSetting.Mute=mute;
        }
        public void SetVolume(float volume)=>UnityEngine.AudioListener.volume=volume;


        public CoroutineHandle<AudioSource> PlayAudio2D(AudioParam audioParam,float fadeInTime=0)
        {
            var audioClipHandle = GetAudioClip(audioParam.audio);
            return EasyCoroutine.Await(audioClipHandle, clip =>
            {
                if (clip == null)
                    return null;
                AudioSource audioSource= FetchAudioSourceByAudioParam(audioParam,AudioSource2DDic);
                audioSource.PlayAudio(clip,audioParam.playMode,fadeInTime);
                return audioSource;
            });
        }
        public CoroutineHandle<AudioSource> PlayAudio2D(EAudioTrack track, string audioName,EAudioPlayMode playMode=EAudioPlayMode.Default,float fadeInTime=0)
        {
            return PlayAudio2D(AudioParam.Default2D(track,audioName,playMode:playMode),fadeInTime);
        }
        public CoroutineHandle<AudioSource> PlayAudio2D(EAudioTrack track, string audioName, bool loop,EAudioPlayMode playMode=EAudioPlayMode.Default,float fadeInTime=0)
        {
            return PlayAudio2D(AudioParam.Default2D(track,audioName,loop,playMode),fadeInTime);
        }
        

        public void StopAudio2D(AudioParam audioParam,float fadeOutTime=0)
        {
            if(GetAudioSource2D(audioParam,out var audioSource))
            {
                audioSource.StopAudio(fadeOutTime);
                WaitEndOfFrameRemoveAudioSourceFromDic(audioParam, AudioSource2DDic);
            }
        }
        public void StopAudios2D(EAudioTrack track, string audioName,float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.StopAudio(fadeOutTime);
                    WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, AudioSource2DDic);
                }
            }
        }
        public void StopAudios2D(EAudioTrack track,float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track)
                {
                    item.Value.StopAudio(fadeOutTime);
                    WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, AudioSource2DDic);
                }
            }
        }
        public void StopAllAudio2D(float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                item.Value.StopAudio(fadeOutTime);
                WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, AudioSource2DDic);
            }
        }
        
        
        public void PauseAudio2D(AudioParam audioParam,float fadeOutTime=0)
        {
            if (GetAudioSource2D(audioParam, out var audioSource))
            {
                audioSource.PauseAudio(fadeOutTime);
            }
        }
        public void PauseAudios2D(EAudioTrack track, string audioName,float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.PauseAudio(fadeOutTime);
                }
            }
        }
        public void PauseAudios2D(EAudioTrack track,float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track)
                {
                    item.Value.PauseAudio(fadeOutTime);
                }
            }
        }
        public void PauseAllAudio2D(float fadeOutTime=0)
        {
            foreach (var item in AudioSource2DDic)
            {
                item.Value.PauseAudio(fadeOutTime);
            }
        }

        
        public void ResumeAudio2D(AudioParam audioParam, float fadeInTime = 0)
        {
            if (GetAudioSource2D(audioParam, out var audioSource))
            {
                audioSource.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAudio2D(EAudioTrack track, string audioName, float fadeInTime = 0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.ResumeAudio(fadeInTime);
                }
            }
        }
        public void ResumeAudios2D(EAudioTrack track, float fadeInTime = 0)
        {
            foreach (var item in AudioSource2DDic)
            {
                if (item.Key.audioTrack == track)
                    item.Value.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAllAudio2D(float fadeInTime = 0)
        {
            foreach (var item in AudioSource2DDic)
            {
                item.Value.ResumeAudio(fadeInTime);
            }
        }


        public CoroutineHandle<AudioSource> PlayAudio3D(GameObject go,AudioParam audioParam,float fadeInTime=0)
        {
            var audioClipHandle = GetAudioClip(audioParam.audio);
            return EasyCoroutine.Await(audioClipHandle, clip =>
            {
                if (clip == null)
                    return null;
                AudioSource audioSource= FetchAudioSource3DByAudioParam(go,audioParam);
                audioSource.PlayAudio(clip,audioParam.playMode,fadeInTime);
                return audioSource;
            });
        }
        public CoroutineHandle<AudioSource> PlayAudio3D(GameObject go,EAudioTrack track, string audioName,EAudioPlayMode playMode=EAudioPlayMode.Default,float fadeInTime=0)
        {
            return PlayAudio3D(go, AudioParam.Default3D(track, audioName,playMode:playMode), fadeInTime);
        }
        public CoroutineHandle<AudioSource> PlayAudio3D(GameObject go,EAudioTrack track, string audioName, bool loop,EAudioPlayMode playMode=EAudioPlayMode.Default,float fadeInTime=0)
        {
            return PlayAudio3D(go, AudioParam.Default3D(track, audioName, loop,playMode), fadeInTime);
        }
        
        
        
        public void StopAudio3D(GameObject go,AudioParam audioParam,float fadeOutTime=0)
        {
            if (!GetAudioSource3D(go, audioParam, out var audioSource))
                return;
            audioSource.StopAudio(fadeOutTime);
            WaitEndOfFrameRemoveAudioSourceFromDic(audioParam, AudioSource3DDic[go]);
        }
        public void StopAudios3D(GameObject go,EAudioTrack track, string audioName,float fadeOutTime=0)
        {
            if(!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.StopAudio(fadeOutTime);
                    WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, goAudioSourceDic);
                }
            }
        }
        public void StopAudios3D(GameObject go, EAudioTrack track, float fadeOutTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audioTrack == track)
                {
                    item.Value.StopAudio(fadeOutTime);
                    WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, goAudioSourceDic);
                }
            }
        }
        public void StopAudios3D(GameObject go, float fadeOutTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                item.Value.StopAudio(fadeOutTime);
                WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, goAudioSourceDic);
            }
        }
        public void StopAudios3D(EAudioTrack track,float fadeOutTime=0)
        {
            foreach (var allGoItem in AudioSource3DDic)
            {
                foreach (var item in allGoItem.Value)
                {
                    if (item.Key.audioTrack == track)
                    {
                        item.Value.StopAudio(fadeOutTime);
                        WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, allGoItem.Value);
                    }
                }
            }
        }
        public void StopAllAudio3D(float fadeOutTime=0)
        {
            foreach (var allGoItem in AudioSource3DDic)
            {
                foreach (var item in allGoItem.Value)
                {
                    item.Value.StopAudio(fadeOutTime);
                    WaitEndOfFrameRemoveAudioSourceFromDic(item.Key, allGoItem.Value);
                }
            }
        }


        public void PauseAudio3D(GameObject go, AudioParam audioParam, float fadeOutTime = 0)
        {
            if (GetAudioSource3D(go,audioParam, out var audioSource))
            {
                audioSource.PauseAudio(fadeOutTime);
            }
        }
        public void PauseAudios3D(GameObject go,EAudioTrack track, string audioName, float fadeOutTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.PauseAudio(fadeOutTime);
                }
            }
        }
        public void PauseAudios3D(GameObject go, EAudioTrack track, float fadeOutTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audioTrack == track)
                {
                    item.Value.PauseAudio(fadeOutTime);
                }
            }
        }
        public void PauseAudios3D(GameObject go, float fadeOutTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                item.Value.PauseAudio(fadeOutTime);
            }
        }
        public void PauseAudios3D(EAudioTrack track,float fadeOutTime = 0)
        {
            foreach (var goAudioSourceDic in AudioSource3DDic.Values)
            {
                foreach (var item in goAudioSourceDic)
                {
                    if (item.Key.audioTrack == track)
                        item.Value.PauseAudio(fadeOutTime);
                }
            }
        }
        public void PauseAllAudio3D(float fadeOutTime = 0)
        {
            foreach (var goAudioSourceDic in AudioSource3DDic.Values)
            {
                foreach (var item in goAudioSourceDic)
                {
                    item.Value.PauseAudio(fadeOutTime);
                }
            }
        }

        
        public void ResumeAudio3D(GameObject go, AudioParam audioParam, float fadeInTime = 0)
        {
            if (GetAudioSource3D(go,audioParam, out var audioSource))
            {
                audioSource.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAudios3D(GameObject go,EAudioTrack track, string audioName, float fadeInTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audioTrack == track && item.Key.audio == audioName)
                {
                    item.Value.ResumeAudio(fadeInTime);
                }
            }
        }
        public void ResumeAudios3D(GameObject go,string audioName,float fadeInTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var goAudioSourceDic))
                return;
            foreach (var item in goAudioSourceDic)
            {
                if (item.Key.audio == audioName)
                    item.Value.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAudios3D(GameObject go,EAudioTrack track, float fadeInTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var allItem))
                return;
            foreach (var item in allItem)
            {
                if (item.Key.audioTrack == track)
                    item.Value.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAudios3D(GameObject go, float fadeInTime = 0)
        {
            if (!AudioSource3DDic.TryGetValue(go, out var allItem))
                return;
            foreach (var item in allItem)
            {
                item.Value.ResumeAudio(fadeInTime);
            }
        }
        public void ResumeAudios3D(EAudioTrack track,float fadeInTime = 0)
        {
            foreach (var goAudioSourceDic in AudioSource3DDic)
            {
                foreach (var item in goAudioSourceDic.Value)
                {
                    if (item.Key.audioTrack == track)
                        item.Value.ResumeAudio(fadeInTime);
                }
            }
        }
        public void ResumeAllAudio3D(float fadeInTime = 0)
        {
            foreach (var goAudioSourceDic in AudioSource3DDic)
            {
                foreach (var item in goAudioSourceDic.Value)
                {
                    item.Value.ResumeAudio(fadeInTime);
                }
            }
        }

        public void ReleaseAudio(string audioPath)
        {
            AudioClipRefCountDic.Remove(audioPath);
        }

        public void ReleaseAll()
        {
            AudioClipRefCountDic.Clear();
        }
        

        public IEasyEvent UpdateEvent => new EasyEvent();
        void IUpdateAble.OnUpdate()
        {
            AudioSource2DDicCheck();
            AudioSource3DDicCheck();
        }

        private void AudioSource2DDicCheck()
        {
            foreach (var item in AudioSource2DDic)
            {
                if (!item.Value || !item.Value.isPlaying && item.Value.time == 0)
                {
                    EasyCoroutine.WaitEndOfFrame(() =>
                    {
                        if (!item.Value || !item.Value.isPlaying && item.Value.time == 0)
                        {
                            AudioSource2DDic.Remove(item.Key);
                            if (item.Value)
                            {
                                item.Value.clip = null;
                                EasyRes.RecycleComponent.InvokeEvent(item.Value);
                            }
                        }
                    });
                }
            }
        }

        private void AudioSource3DDicCheck()
        {
            foreach (var goItem in AudioSource3DDic)
            {
                foreach (var item in goItem.Value)
                {
                    if (!item.Value || !item.Value.isPlaying && item.Value.time == 0)
                    {
                        EasyCoroutine.WaitEndOfFrame(() =>
                        {
                            if (!item.Value || !item.Value.isPlaying && item.Value.time == 0)
                            {
                                goItem.Value.Remove(item.Key);
                                if (goItem.Value.Count == 0)
                                    AudioSource3DDic.Remove(goItem.Key);
                                if (item.Value)
                                {
                                    item.Value.clip = null;
                                    EasyRes.RecycleComponent.InvokeEvent(item.Value);
                                }
                            }
                        });
                    }
                }

                if (!goItem.Key)
                    EasyCoroutine.WaitEndOfFrame(() =>
                    {
                        if (!goItem.Key)
                            AudioSource3DDic.Remove(goItem.Key);
                    });
            }
        }
        protected override void OnInit()
        {
            AudioSystemSetting.Instance.Init();
        }

        protected override void OnActive()
        {
            EasyRes.ReleaseAsset.RegisterEvent(ReleaseAudio);
            EasyRes.ReleaseAll.RegisterEvent(ReleaseAll);
            _audioClipRefCountTimerHandle = EasyCoroutine.Loop(()=>
                EasyCoroutine
                    .Delay(180, false)
                    .OnCompleted(() =>
                    {
                        Action complete = null;
                        foreach (var item in AudioClipRefCountDic)
                        {
                            item.Value.RefCount--;
                            if (item.Value.RefCount <= 0)
                                complete += () => AudioClipRefCountDic.Remove(item.Key);
                        }
                        complete?.Invoke();
                    })
                , 0);
        }

        protected override void OnUnActive()
        {
            EasyRes.ReleaseAsset.UnRegisterEvent(ReleaseAudio);
            EasyRes.ReleaseAll.UnRegisterEvent(ReleaseAll);
            _audioClipRefCountTimerHandle.Cancel();
            _audioClipRefCountTimerHandle = null;
            AudioClipRefCountDic.Clear();
        }

        protected override void OnDispose(bool usePool)
        {
            StopAllAudio2D();
            StopAllAudio3D();
            AudioSource2DDic.Clear();
            AudioSource3DDic.Clear();
            AudioSourceParamDic.Clear();
        }
    }

    public static class EasyAudioSystemExtension
    {
        public static AudioSource SetParam(this AudioSource audioSource, AudioParam audioParam)
        {
            var audioSystemData = AudioSystemSetting.GetAudioSystemData(audioParam.audioTrack);
            audioSource.loop = audioParam.loop;
            audioSource.priority = audioParam.priority;
            audioSource.mute = audioSystemData.IsMute;
            audioSource.volume = 0;
            audioSource.pitch = audioParam.pitch;
            audioSource.panStereo = audioParam.stereoPan;
            audioSource.spatialBlend = audioParam.spatialBlend;
            audioSource.reverbZoneMix = audioParam.reverbZoneMix;
            audioSource.dopplerLevel = audioParam.dopplerLevel;
            audioSource.spread = audioParam.spread;
            audioSource.rolloffMode = audioParam.rolloffMode;
            audioSource.minDistance = audioParam.minDistance;
            audioSource.maxDistance = audioParam.maxDistance;
            var audioSourceData = EasyAudioSystem.GetAudioSourceData(audioSource);
            audioSourceData.AudioParam = audioParam;
            audioSourceData.Volume = audioParam.volume;
            audioSource.RequestRefreshAudioSource(audioSourceData, audioSystemData);
            return audioSource;
        }

        private static void RefreshAudioSource(this AudioSource audioSource, AudioSourceData data, AudioSystemData audioSystemData)
        {
            data.NeedRefresh = false;
            audioSource.volume = Mathf.Clamp01(data.Volume * audioSystemData.Volume);
        }

        public static void RequestRefreshAudioSource(this AudioSource audioSource,AudioSourceData data,AudioSystemData audioSystemData)
        {
            if(data.NeedRefresh||!audioSource)
                return;
            data.NeedRefresh = true;
            EasyCoroutine.WaitEndOfFrame(() =>audioSource.RefreshAudioSource(data,audioSystemData));
        }
        public static void RequestRefreshAudioSource(this AudioSource audioSource,AudioSourceData data)
        {
            if(data.NeedRefresh||!audioSource)
                return;
            ref var needRefresh = ref data.NeedRefresh;
            needRefresh = true;
            var audioSystemData = AudioSystemSetting.GetAudioSystemData(data.AudioParam.audioTrack);
            EasyCoroutine.WaitEndOfFrame(() =>audioSource.RefreshAudioSource(data,audioSystemData));
        }
        public static void RequestRefreshAudioSource(this AudioSource audioSource)
        {
            var data= EasyAudioSystem.GetAudioSourceData(audioSource);
            audioSource.RequestRefreshAudioSource(data);
        }
        
        
        internal static AudioSource PlayAudio(this AudioSource audioSource, AudioClip audioClip,EAudioPlayMode playMode,float fadeInTime)
        {
            if (audioSource == null || audioClip == null)
                return audioSource;
            audioSource.clip = audioClip;
            
            audioSource.PlayAudio(playMode, fadeInTime);
            return audioSource;
        }
        private static void PlayAudio(this AudioSource audioSource,EAudioPlayMode playMode,float fadeInTime)
        {
            if (!audioSource
                ||audioSource.clip == null)
                return;

            if (!audioSource.isPlaying)
            {
                var data = EasyAudioSystem.GetAudioSourceData(audioSource);
                audioSource.time = Mathf.Clamp(data.AudioParam.startTime, 0, audioSource.clip.length - 0.01f);
                audioSource.FadeVolume(0, data.Volume, fadeInTime);
            }
            
            EasyAudioSystem.AudioListener();
            switch (playMode)
            {
                case EAudioPlayMode.PlayOneShot:
                    audioSource.PlayOneShot(audioSource.clip);
                    break;
                case EAudioPlayMode.Default:
                    if (!audioSource.isPlaying)
                        audioSource.Play();
                    break;
                case EAudioPlayMode.Replay:
                    if (audioSource.isPlaying)
                        audioSource.time = 0;
                    audioSource.Play();
                    break;
            }
        }
        internal static void ResumeAudio(this AudioSource audioSource, float fadeInTime)
        {
            if (!audioSource
                || audioSource.clip == null
                || audioSource.isPlaying)
                return;

            var audioParam = EasyAudioSystem.GetAudioParam(audioSource);
            var audioSystemData = AudioSystemSetting.GetAudioSystemData(audioParam.audioTrack);
            var targetVolume = Mathf.Clamp01(audioParam.volume *audioSystemData.Volume);
            audioSource.mute = audioSystemData.IsMute;
            if (!audioSource.isPlaying)
                audioSource.FadeVolume(0, targetVolume, fadeInTime);
            
            audioSource.Play();
        }
        internal static void PauseAudio(this AudioSource audioSource, float fadeOutTime)
        {
            if (!audioSource
                || audioSource.clip == null
                || !audioSource.isPlaying) 
                return;
            audioSource.FadeVolume(audioSource.volume, 0, fadeOutTime, Pause);
            void Pause()
            {
                if(!audioSource)
                    return;
                audioSource.Pause();
            }
        }
        internal static void StopAudio(this AudioSource audioSource, float fadeOutTime)
        {
            if (!audioSource
                || audioSource.clip == null) 
                return;
            audioSource.FadeVolume(audioSource.volume, 0, fadeOutTime, Stop);

            void Stop()
            {
                if(!audioSource)
                    return;
                audioSource.Stop();
                audioSource.clip = null;
                EasyRes.RecycleComponent.InvokeEvent(audioSource);
            }
        }

        public static void SetVolume(this AudioSource audioSource, float volume)
        {
            var data = EasyAudioSystem.GetAudioSourceData(audioSource);
            audioSource.SetVolume(data,volume);
        }
        public static void SetVolume(this AudioSource audioSource,AudioSourceData data, float volume)
        {
            data.Volume = volume;
            audioSource.RequestRefreshAudioSource(data);
        }

        private static CoroutineHandle FadeVolume(this AudioSource audioSource, float startVolume, float endVolume, float duration,Action onCompleted = null)
        {
            var data = EasyAudioSystem.GetAudioSourceData(audioSource);
            data.AudioParam.VolumeCoroutineHandle.Cancel();
            if (duration > 0)
            {
                audioSource.SetVolume(data,startVolume);
                data.AudioParam.VolumeCoroutineHandle = EasyCoroutine.TimeTask(duration, t =>
                {
                    if (audioSource == null)
                        return;
                    audioSource.SetVolume(data,Mathf.Lerp(startVolume, endVolume, t));
                }, false);
                data.AudioParam.VolumeCoroutineHandle.OnCompleted(onCompleted);
                data.AudioParam.VolumeCoroutineHandle.OnDone(() => data.AudioParam.VolumeCoroutineHandle = null);
            }
            else
            {
                audioSource.SetVolume(data,endVolume);
                onCompleted?.Invoke();
            }

            return data.AudioParam.VolumeCoroutineHandle;
        }
    }
}