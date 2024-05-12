using System;
using UnityEngine;

namespace EasyFramework
{
    public class Delay: AEasyProgressAction
    {
        private Delay()
        {
            ActionID = EasyActionSingleton.GetActionId();
        }

        private static EasyPool<Delay> _pool = new(() => new Delay(), null, null);

        public static Delay Fetch(float duration, Action action,int loopCount = 1)
        {
            var delay = _pool.Fetch();
            delay._delayAction = action;
            delay.Reset(duration,loopCount);
            return delay;
        }
        
        private Action _delayAction;

        protected override void OnActionCompleted()
        {
            _delayAction?.Invoke();
            RunTime=0;
        }
        protected override void OnActionCancel(){}
        protected override void OnActionEnd(){}
        protected override bool OnActionUpdate(float deltaTime)
        {
            Progress=Mathf.Clamp01(RunTime/Duration);
            return Progress >= 1;
        }
    }
    
    public static class DelayExtension
    {
        public static Sequence Delay(this Sequence self, float duration, Action action,Action<Delay> set=null)
        {
            var delay = EasyFramework.Delay.Fetch(duration, action);
            set?.Invoke(delay);
            return self.Append(delay);
        }
    }
}