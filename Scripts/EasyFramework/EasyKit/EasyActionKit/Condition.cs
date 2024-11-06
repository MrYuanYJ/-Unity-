using System;
using System.Net.NetworkInformation;

namespace EasyFramework
{
    public class Condition : AEasyAction
    {
        private Condition()
        {
            ActionID = EasyActionSingleton.GetActionId();
        }

        private static EasyPool<Condition> _pool = new(() => new Condition(), null, null);

        public static Condition Fetch(Func<bool> conditionFunc, int loopCount = 1)
        {
            var condition = _pool.Fetch();
            condition.ConditionFunc = conditionFunc;
            
            condition.Reset(loopCount);
            return condition;
        }


        private Func<bool> ConditionFunc { get; set; }

        protected override void OnActionCompleted()=> RunTime = 0;

        protected override void OnActionCancel(){}

        protected override void OnActionEnd(){}

        protected override bool OnActionUpdate(float deltaTime)=>ConditionFunc();
    }

    public static class ConditionExtensions
    {
        public static Sequence Condition(this Sequence self, Func<bool> conditionFunc,Action<Condition> set=null)
        {
            var condition = EasyFramework.Condition.Fetch(conditionFunc);
            set?.Invoke(condition);
            return self.Append(condition);
        }
        public static Sequence Action(this Sequence self, Action action, Action<Condition> set = null)
        {
            var condition = EasyFramework.Condition.Fetch(() => true);
            condition.OnCompleted(action);
            set?.Invoke(condition);
            return self.Append(condition);
        }
    }
}