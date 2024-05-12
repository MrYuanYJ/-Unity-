using UnityEngine;

namespace EasyFramework
{
    public interface IEase
    {
        bool UsePresets { get; }
        EaseType EaseType { get; }
        AnimationCurve AnimationCurve { get; }
        void SetEase(EaseType easeType);
        void SetEase(AnimationCurve animationCurve);
        float Evaluate(float input) => input;
        float Sine(int count,float input)=>Mathf.Sin(count*input*Mathf.PI*2);
        float Cosine(int count,float input)=>Mathf.Cos(count*input*Mathf.PI*2);
    }
}