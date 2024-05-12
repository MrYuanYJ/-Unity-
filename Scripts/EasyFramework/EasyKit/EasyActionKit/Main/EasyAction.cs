using System;
using UnityEngine;

namespace EasyFramework
{
    public static class EasyAction
    {
        public static Delay Delay(float duration, Action action, int loopCount)
        {
            return EasyFramework.Delay.Fetch(duration, action, loopCount);
        }

        public static Delay Delay(float duration, Action action) { return EasyFramework.Delay.Fetch(duration, action); }

        public static Condition Condition(Func<bool> condition, int loopCount)
        {
            return EasyFramework.Condition.Fetch(condition, loopCount);
        }

        public static Condition Condition(Func<bool> condition) { return EasyFramework.Condition.Fetch(condition); }


        public static Sequence Sequence(int loopCount, LoopType loopType)
        {
            return EasyFramework.Sequence.Fetch(loopCount, loopType);
        }

        public static Sequence Sequence(int loopCount) { return EasyFramework.Sequence.Fetch(loopCount); }
        public static Sequence Sequence() { return EasyFramework.Sequence.Fetch(); }

        public static Lerp<T> Lerp<T>(float duration, T start, T end, Action<T> onValueChanged,
            Func<Lerp<T>, T> getValue,
            int loopCount, LoopType loopType)
        {
            return EasyFramework.Lerp<T>.Fetch(duration, start, end, onValueChanged, getValue, loopCount, loopType);
        }

        public static Lerp<T> Lerp<T>(float duration, T start, T end, Action<T> onValueChanged,
            Func<Lerp<T>, T> getValue,
            int loopCount)
        {
            return EasyFramework.Lerp<T>.Fetch(duration, start, end, onValueChanged, getValue, loopCount);
        }

        public static Lerp<T> Lerp<T>(float duration, T start, T end, Action<T> onValueChanged,
            Func<Lerp<T>, T> getValue)
        {
            return EasyFramework.Lerp<T>.Fetch(duration, start, end, onValueChanged, getValue);
        }

        public static Lerp<T> Lerp<T>(float duration, T end, Action<T> onValueChanged, Func<Lerp<T>, T> getValue)
        {
            return EasyFramework.Lerp<T>.Fetch(duration, default, end, onValueChanged, getValue);
        }

        public static Lerp<float> Float(float duration, float start, float end, Action<float> onValueChanged)
        {
            return EasyFramework.Lerp<float>.Fetch(duration, start, end, onValueChanged,
                lerp => Mathf.LerpUnclamped(lerp.StartValue, lerp.EndValue, lerp.Ease.Evaluate(lerp.Progress)));
        }

        public static Lerp<float> Float(float duration, float end, Action<float> onValueChanged)
        {
            return EasyFramework.Lerp<float>.Fetch(duration, 0, end, onValueChanged,
                lerp => Mathf.LerpUnclamped(lerp.StartValue, lerp.EndValue, lerp.Ease.Evaluate(lerp.Progress)));
        }
        
        public static Lerp<int> Int(float duration, int start, int end, Action<int> onValueChanged)
        {
            return EasyFramework.Lerp<int>.Fetch(duration, start, end, onValueChanged,
                lerp => Mathf.CeilToInt(Mathf.LerpUnclamped(lerp.StartValue, lerp.EndValue, lerp.Ease.Evaluate(lerp.Progress))));
        }

        public static Lerp<int> Int(float duration, int end, Action<int> onValueChanged)
        {
            return EasyFramework.Lerp<int>.Fetch(duration, 0, end, onValueChanged,
                lerp => Mathf.CeilToInt(Mathf.LerpUnclamped(lerp.StartValue, lerp.EndValue, lerp.Ease.Evaluate(lerp.Progress))));
        }
        
        public static Lerp<Vector2> Vector2(float duration, Vector2 start, Vector2 end, Action<Vector2> onValueChanged)
        {
            return EasyFramework.Lerp<Vector2>.Fetch(duration, start, end, onValueChanged,
                lerp => UnityEngine.Vector2.LerpUnclamped(lerp.StartValue, lerp.EndValue,  lerp.Ease.Evaluate(lerp.Progress)));
        }

        public static Lerp<Vector2> Vector2(float duration, Vector2 end, Action<Vector2> onValueChanged)
        {
            return EasyFramework.Lerp<Vector2>.Fetch(duration, UnityEngine.Vector2.zero, end, onValueChanged,
                lerp => UnityEngine.Vector2.LerpUnclamped(lerp.StartValue, lerp.EndValue,  lerp.Ease.Evaluate(lerp.Progress)));
        }
        
        public static Lerp<Vector3> Vector3(float duration, Vector3 start, Vector3 end, Action<Vector3> onValueChanged)
        {
            return EasyFramework.Lerp<Vector3>.Fetch(duration, start, end, onValueChanged,
                lerp => UnityEngine.Vector3.LerpUnclamped(lerp.StartValue, lerp.EndValue,  lerp.Ease.Evaluate(lerp.Progress)));
        }

        public static Lerp<Vector3> Vector3(float duration, Vector3 end, Action<Vector3> onValueChanged)
        {
            return EasyFramework.Lerp<Vector3>.Fetch(duration, UnityEngine.Vector3.zero, end, onValueChanged,
                lerp => UnityEngine.Vector3.LerpUnclamped(lerp.StartValue, lerp.EndValue,  lerp.Ease.Evaluate(lerp.Progress)));
        }

        public static DirMove DirMove(Transform transform, Vector3 dir, float duration, int loopCount, LoopType loopType)
        {
            return EasyFramework.DirMove.Fetch(transform, dir, duration, loopCount, loopType);
        }
        public static DirMove DirMove(Transform transform, Vector3 dir, float duration, int loopCount)
        {
            return EasyFramework.DirMove.Fetch(transform, dir, duration, loopCount,EasyFramework.LoopType.ReStart);
        }
        public static DirMove DirMove(Transform transform, Vector3 dir, float duration)
        {
            return EasyFramework.DirMove.Fetch(transform, dir, duration, 1, EasyFramework.LoopType.ReStart);
        }

        #region EasyActionExtension


        public static void Start<T>(this T self) where T : IEasyAction
        {
            self.Start();
        }
        public static T Pause<T>(this T self) where T : IEasyAction
        {
            self.Pause();
            return self;
        }
        public static T Resume<T>(this T self) where T : IEasyAction
        {
            self.Resume();
            return self;
        }
        public static T Cancel<T>(this T self) where T : IEasyAction
        {
            self.Cancel();
            return self;
        }
        public static T LoopCount<T>(this T self, int loopCount) where T : IEasyAction
        {
            self.SetLoopCount(loopCount);
            return self;
        }
        public static T LoopType<T>(this T self, LoopType loopType) where T : IEasyExLoopAction
        {
            self.SetLoopType(loopType);
            return self;
        }
        public static T Ease<T>(this T self, EaseType easeType) where T : IEasyLerpAction
        {
            self.Ease.SetEase(easeType);
            return self;
        }
        public static T Ease<T>(this T self, AnimationCurve animationCurve) where T : IEasyLerpAction
        {
            self.Ease.SetEase(animationCurve);
            return self;
        }
        public static T Update<T>(this T self, Action action) where T : IEasyActionEvent
        {
            self.OnRunning+= action;
            return self;
        }
        public static T Complete<T>(this T self, Action action) where T : IEasyActionEvent
        {
            self.OnCompleted+= action;
            return self;
        }
        public static T Canceled<T>(this T self, Action action) where T : IEasyActionEvent
        {
            self.OnCanceled+= action;
            return self;
        }
        public static T End<T>(this T self, Action action) where T : IEasyActionEvent
        {
            self.OnEnd+= action;
            return self;
        }
        

        #endregion

    }
}