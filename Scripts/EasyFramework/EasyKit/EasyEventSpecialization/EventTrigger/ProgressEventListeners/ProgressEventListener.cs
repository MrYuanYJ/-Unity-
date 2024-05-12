using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EasyFramework.EventKit
{
    public class ProgressEventListener: MonoBehaviour,IProgressEvent<float>
    {
        private float _progress;
        private EasyEvent<float> easyEvent=new EasyEvent<float>(); 
        [FormerlySerializedAs("easeData")] [FormerlySerializedAs("_ease")] [SerializeField] private Ease ease = new Ease();
        [SerializeField] private UnityEvent<float> registerEvent=new UnityEvent<float>();
        [SerializeField] private float min=0;
        [SerializeField] private float max=1;

        public Ease Ease => ease;

        [ProgressBar(0,1,Height = 20)]
        [HideLabel]
        [ShowInInspector]
        public float Progress
        {
            get => _progress;
            set => SetProgress(value);
        }

        public EasyEvent<float> Event => easyEvent;
        public UnityEvent<float> Register => registerEvent;
        
        public void SetProgress(float progress)
        {
            _progress = Mathf.Clamp01(progress);
            var easedProgress = Ease.Evaluate(progress)*(max-min)+min;
            Event?.Invoke(easedProgress);
            Register?.Invoke(easedProgress);
        }
    }
}