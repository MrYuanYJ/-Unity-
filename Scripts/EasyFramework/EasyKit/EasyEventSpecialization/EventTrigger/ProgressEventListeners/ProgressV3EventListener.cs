using EXFunctionKit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EasyFramework
{
    public class ProgressV3EventListener: MonoBehaviour,IProgressEvent<Vector3>
    {
        private float _progress;
        private EasyEvent<Vector3> easyEvent=new EasyEvent<Vector3>(); 
        [FormerlySerializedAs("easeData")] [FormerlySerializedAs("_ease")] [SerializeField] private Ease ease = new Ease();
        [SerializeField] private UnityEvent<Vector3> registerEvent=new UnityEvent<Vector3>();
        [SerializeField] private Vector3 min=Vector3.zero;
        [SerializeField] private Vector3 max=Vector3.one;

        public Ease Ease => ease;

        [ProgressBar(0,1,Height = 20)]
        [HideLabel]
        [ShowInInspector]
        public float Progress
        {
            get => _progress;
            set => SetProgress(value);
        }

        public EasyEvent<Vector3> Event => easyEvent;
        public UnityEvent<Vector3> Register => registerEvent;
        
        public void SetProgress(float progress)
        {
            _progress = Mathf.Clamp(progress, 0, 1);
            var easedProgress = Ease.Evaluate(_progress).LerpVector3(min, max);
            Event?.Invoke(easedProgress);
            Register?.Invoke(easedProgress);
        }
    }
}