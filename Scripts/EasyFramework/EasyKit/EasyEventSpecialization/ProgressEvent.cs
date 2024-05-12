
namespace EasyFramework.EventKit
{
    public interface IProgressEvent<T>
    {
        Ease Ease { get;}
        EasyEvent<T> Event { get;}
        float Progress { get; set; }
        
        void SetProgress(float progress);
    }


    public class ProgressEvent : IProgressEvent<float>
    {
        private float _progress;
        private Ease _ease= new Ease();
        private EasyEvent<float> _event= new EasyEvent<float>();

        public Ease Ease => _ease;
        public EasyEvent<float> Event => _event;

        public float Progress
        {
            get=> _progress;
            set=> SetProgress(value);
        }

        public void SetProgress(float progress)
        {
           _progress = progress;
           Event.Invoke(Ease.Evaluate(progress));
        }
    }
}