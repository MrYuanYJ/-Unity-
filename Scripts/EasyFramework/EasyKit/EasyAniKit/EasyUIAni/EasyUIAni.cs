using System;
using System.Collections.Generic;
using EasyFramework.EasyUIKit;
using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public class EasyUIAni: IEasyAni
    {
        public EasyUIAni(){}
        public EasyUIAni(bool isShow)
        {
#if UNITY_EDITOR

            if(isShow)
                foreach (var ani in EasyUIKitSetting.DefaultUIShowAni) { anis.Add(ani); }
            else
                foreach (var ani in EasyUIKitSetting.DefaultUIHideAni) { anis.Add(ani); }
            isUnscaledTime=EasyUIKitSetting.DefaultIsIgnoreTimeScale;
            duration=EasyUIKitSetting.DefaultDuration;
#endif
        }
        public float Duration => duration;
        public bool IsUnscaledTime => isUnscaledTime;
        public IEnumerable<ISingleAni> Anis => anis;
        public IEasyPanel Panel{ get; private set; }
        public event Action<float> Playing;
        public event Action Finished;


        [SerializeField] private List<SingleUIAni> anis = new();
        [SerializeField] private bool isUnscaledTime;
        [SerializeField] private float duration;

        public CoroutineHandle Run(Action onFinished = null, params object[] args)
        {
            Panel = args[0] as IEasyPanel;
            var handle=EasyTask.TimeTask(Duration, Play, IsUnscaledTime);
            Finished += onFinished;
            handle.Completed += () => ((IEasyAni)this).Finish();
            return handle;
        }
        private void Play(float progress)
        {
            foreach (var ani in Anis)
                ani.Playing(ani.Ease.Evaluate(progress), Panel);
            Playing?.Invoke(progress);
        }
        public void Finish()
        {
            Finished?.Invoke();
            Finished = null;
        }
    }
}