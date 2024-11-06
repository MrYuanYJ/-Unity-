using EXFunctionKit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EasyFramework
{
    public class ProgressIntEventListener: MonoBehaviour,IProgressEvent<int>
    {
        private float _progress;
        private EasyEvent<int> easyEvent=new EasyEvent<int>(); 
        [FormerlySerializedAs("easeData")] [FormerlySerializedAs("_ease")] [SerializeField] private Ease ease = new Ease();
        [SerializeField] private UnityEvent<int> registerEvent=new UnityEvent<int>();
        [SerializeField] private int min=0;
        [SerializeField] private int max=10;

        public Ease Ease => ease;

        [ProgressBar(0,1,Height = 20)]
        [HideLabel]
        [ShowInInspector]
        public float Progress
        {
            get => _progress;
            set => SetProgress(value);
        }

        public EasyEvent<int> Event => easyEvent;
        public UnityEvent<int> Register => registerEvent;
        
        public void SetProgress(float progress)
        {
            _progress = progress;
            var easedProgress = (Ease.Evaluate(progress)*(max-min)+min).AsRoundInt();
            Event?.Invoke(easedProgress);
            Register?.Invoke(easedProgress);
        }
    }
}