using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public class Ease:IEase
    {
        public Ease(){}
        public Ease(EaseType easeType)
        {
            SetEase(easeType);
        }
        public Ease(AnimationCurve animationCurve)
        {
            SetEase(animationCurve);
        }
        
        public bool UsePresets => usePresets;
        public EaseType EaseType => easeType;
        public AnimationCurve AnimationCurve => animationCurve;

        [SerializeField] private bool usePresets=true;
        [ShowIf("@usePresets == true")][SerializeField] private EaseType easeType;
        [ShowIf("@usePresets == false")][SerializeField] private AnimationCurve animationCurve;

        public void SetEase(EaseType easeType)
        {
            usePresets=true;
            this.easeType = easeType;
        }

        public void SetEase(AnimationCurve animationCurve)
        {
            usePresets=false;
            this.animationCurve = animationCurve;
        }

        public float Evaluate(float time)
        {
            if (usePresets)
            {
                return Evaluate(easeType, time);
            }
            else
            {
                return animationCurve.Evaluate(time);
            }
        }

        public static float Sine(int count,float input)=>Mathf.Sin(count*input*Mathf.PI*2);
        public static float Cosine(int count,float input)=>Mathf.Cos(count*input*Mathf.PI*2);
        public static float Evaluate(EaseType easeType, float time)
        {
            switch (easeType)
            {
                case EaseType.Linear:
                    return time;
                case EaseType.EaseInSine:
                    return 1 - Mathf.Cos((time * Mathf.PI) / 2);
                case EaseType.EaseOutSine:
                    return Mathf.Sin((time * Mathf.PI) / 2);
                case EaseType.EaseInOutSine:
                    return -(Mathf.Cos(Mathf.PI * time) - 1) / 2;
                case EaseType.EaseInQuad:
                    return time * time;
                case EaseType.EaseOutQuad:
                    return 1 - (1 - time) * (1 - time);
                case EaseType.EaseInOutQuad:
                    return time < 0.5 ? 2 * time * time : 1 - Mathf.Pow(-2 * time + 2, 2) / 2;
                case EaseType.EaseInCubic:
                    return time * time * time;
                case EaseType.EaseOutCubic:
                    return 1 - Mathf.Pow(1 - time, 3);
                case EaseType.EaseInOutCubic:
                    return time < 0.5 ? 4 * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 3) / 2;
                case EaseType.EaseInQuart:
                    return time * time * time * time;
                case EaseType.EaseOutQuart:
                    return 1 - Mathf.Pow(1 - time, 4);
                case EaseType.EaseInOutQuart:
                    return time < 0.5 ? 8 * time * time : 1 - Mathf.Pow(-2 * time + 2, 4) / 2;
                case EaseType.EaseInQuint:
                    return time * time * time * time * time;
                case EaseType.EaseOutQuint:
                    return 1 - Mathf.Pow(1 - time, 5);
                case EaseType.EaseInOutQuint:
                    return time < 0.5 ? 16 * time * time : 1 - Mathf.Pow(-2 * time + 2, 5) / 2;
                case EaseType.EaseInExpo:
                    return time == 0 ? 0 : Mathf.Pow(2, 10 * time - 10);
                case EaseType.EaseOutExpo:
                    return time == 1 ? 1 : 1 - Mathf.Pow(2, -10 * time);
                case EaseType.EaseInOutExpo:
                    if (time == 0) return 0;
                    if (time == 1) return 1;
                    if (time < 0.5) return Mathf.Pow(2, 20 * time - 10) / 2;
                    return (2 - Mathf.Pow(2, -20 * time + 10)) / 2;
                case EaseType.EaseInCirc:
                    return 1 - Mathf.Sqrt(1 - Mathf.Pow(time, 2));
                case EaseType.EaseOutCirc:
                    return Mathf.Sqrt(1 - Mathf.Pow(time - 1, 2));
                case EaseType.EaseInOutCirc:
                    if (time < 0.5) return (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * time, 2))) / 2;
                    return (Mathf.Sqrt(1 - Mathf.Pow(-2 * time + 2, 2)) + 1) / 2;
                case EaseType.EaseInElastic:
                    return Mathf.Sin(13 * Mathf.PI / 2 * time) * Mathf.Pow(2, 10 * (time - 1));
                case EaseType.EaseOutElastic:
                    return Mathf.Sin(-13 * Mathf.PI / 2 * (time + 1)) * Mathf.Pow(2, -10 * time) + 1;
                case EaseType.EaseInOutElastic:
                    if (time < 0.5)
                        return (Mathf.Sin(13 * Mathf.PI / 2 * (2 * time)) * Mathf.Pow(2, 10 * (2 * time - 1))) / 2;
                    return (Mathf.Sin(-13 * Mathf.PI / 2 * ((2 * time - 1) + 1)) * Mathf.Pow(2, -10 * (2 * time - 1)) +
                            2) / 2;
                case EaseType.EaseInBack:
                    return time * time * ((1.70158f + 1) * time - 1.70158f);
                case EaseType.EaseOutBack:
                    return 1 + time * time * ((1.70158f + 1) * time + 1.70158f);
                case EaseType.EaseInOutBack:
                    return time < 0.5f
                        ? (time * time * ((2.5949095f + 1) * 2 * time - 2.5949095f)) / 2
                        : (1 + time * time * ((2.5949095f + 1) * (2 * time - 2) + 2.5949095f)) / 2;
                case EaseType.EaseInBounce:
                    return 1 - Evaluate(EaseType.EaseOutBounce, 1 - time);
                case EaseType.EaseOutBounce:
                    if (time < 4 / 11.0f) return 121 * time * time / 16.0f;
                    if (time < 8 / 11.0f) return (363 / 40.0f * time * time) - (99 / 10.0f * time) + 17 / 5.0f;
                    if (time < 9 / 10.0f)
                        return (4356 / 361.0f * time * time) - (35442 / 1805.0f * time) + 16061 / 1805.0f;
                    return (54 / 5.0f * time * time) - (513 / 25.0f * time) + 268 / 25.0f;
                case EaseType.EaseInOutBounce:
                    if (time < 0.5f) return (1 - Evaluate(EaseType.EaseOutBounce, 1 - 2 * time)) / 2;
                    return (1 + Evaluate(EaseType.EaseOutBounce, 2 * time - 1)) / 2;
                case EaseType.Sin:
                    return Sine(1, time);
                case EaseType.Cos:
                    return Cosine(1, time);
                default:
                    return 1;
            }
        }
    }
    

    public enum EaseType
    {
        Linear,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,
        Sin,
        Cos,
        None
    }
}