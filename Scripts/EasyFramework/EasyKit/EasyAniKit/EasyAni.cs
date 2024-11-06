using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EasyFramework
{
    [System.Serializable]
    public class EasyAni: IEasyAni
    {
        public EasyAni(float duration, bool isUnscaledTime, List<SingleAni> anis)
        {
            this.duration = duration;
            this.isUnscaledTime = isUnscaledTime;
            this.anis = anis;
        }
        public float Duration => duration;
        public bool IsUnscaledTime => isUnscaledTime;
        public IEnumerable<ISingleAni> Anis => anis;
        public event Action<float> Playing;
        public event Action Finished;
        
        
        [SerializeField]private float duration;
        [SerializeField]private bool isUnscaledTime;
        [SerializeField]private UnityEvent finished;
        [SerializeField]private List<SingleAni> anis;
        

        public CoroutineHandle Run(Action onFinished, params object[] args)
        {
            var handle=EasyTask.TimeTask(Duration, Play, IsUnscaledTime);
            Finished += onFinished;
            handle.Completed += () => ((IEasyAni)this).Finish();
            return handle;
        }

        private void Play(float progress)
        {
            foreach (var ani in Anis)
                ani.Playing(ani.Ease.Evaluate(progress));
            Playing?.Invoke(progress);
        }
        void IEasyAni.Finish()
        {
            finished?.Invoke();   
            Finished?.Invoke();
            finished?.RemoveAllListeners();
            Finished = null;
        }
    }
}