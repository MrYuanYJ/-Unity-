using System;
using UnityEngine;

namespace EasyFramework
{
    public class Lerp<T> : AEasyProgressAndLerpAndExLoopAction<T>
    {
        private Lerp()
        {
            ActionID = EasyActionSingleton.GetActionId();
        }
        private static EasyPool<Lerp<T>> _pool = new(() => new Lerp<T>(), null, null);

        public static Lerp<T> Fetch(float duration, T startValue, T endValue, Action<T> onUpdate,
            Func<Lerp<T>, T> onGetValue, int loopCount = 1, LoopType loopType = LoopType.ReStart)
        {
            var lerp = _pool.Fetch();
            lerp._onUpdate = onUpdate;
            lerp._onGetValue = onGetValue;
            lerp.Reset(duration, loopCount, startValue, endValue, loopType);
            
            return lerp;
        }
        
        protected Action<T> _onUpdate;
        protected Func<Lerp<T>,T> _onGetValue;

        protected override void OnActionEnd(){}
        protected override void OnActionCompleted()
        {
            if (LoopType == LoopType.ReStart)
                RunTime = 0;
            else if (LoopType == LoopType.YoYo)
            {
                RunTime = 0;
                IsReverse =false;
            }
            else if (LoopType == LoopType.Incremental)
            {
                MaxProgress++;
            }
        }

        protected override void OnActionCancel(){}

        protected override bool OnActionUpdate(float deltaTime)
        {
            if (IsReverse)
            {
                Progress = 1 - Mathf.Clamp01(RunTime / Duration);
                _onUpdate?.Invoke(GetValue());
                return Progress <= 0;
            }
            else
            {
                Progress = Mathf.Clamp(RunTime / Duration, 0, MaxProgress);
                _onUpdate?.Invoke(GetValue());
                if (Progress >= MaxProgress)
                {
                    if (LoopType == LoopType.YoYo)
                    {
                        Progress = MaxProgress;
                        RunTime = 0;
                        IsReverse = true;
                        return false;
                    }
                    else return true;
                }

                return false;
            }
        }

        public override T GetValue()=> _onGetValue.Invoke(this);
    }


    public static class LerpExtensions
    {
        public static Sequence Lerp<T>(this Sequence self, float duration, T startValue, T endValue, Action<T> onUpdate,Func<Lerp<T>, T> getValue,Action<Lerp<T>> set=null)
        {
            var lerp = EasyFramework.Lerp<T>.Fetch(duration, startValue, endValue, onUpdate, getValue);
            set?.Invoke(lerp);
            return self.Append(lerp);
        }
    }
}