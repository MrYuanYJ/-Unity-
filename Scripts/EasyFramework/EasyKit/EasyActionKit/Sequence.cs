using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class Sequence: AEasyExLoopAction, IEasyAction
    {
        private Sequence()
        {
            ActionID = EasyActionSingleton.GetActionId();
        }
        private static EasyPool<Sequence> _pool = new EasyPool<Sequence>(()=>new Sequence(), null, null);

        public static Sequence Fetch(int loopCount = 1, LoopType loopType = LoopType.ReStart)
        {
            var sequence = _pool.Fetch();
            sequence._actions = new List<IEasyAction>();
            sequence._currentIndex = 0;
            sequence.Reset(loopCount, loopType);
            
            return sequence;
        }
        
        private List<IEasyAction> _actions;
        private int _currentIndex;

        public Sequence Append(IEasyAction action)
        {
            _actions.Add(action);
            return this;
        }

        void IEasyAction.Start()
        {
            if (_actions.Count > 0)
                EasyActionSingleton.AddAction(this);
        }

        protected override void OnActionCompleted()
        {
            foreach (var action in _actions)
            {
                action.SetCurrentLoopCount(0);
            }
            _currentIndex = 0;
            if (LoopType == LoopType.YoYo)
                IsReverse = false;
            else if (LoopType == LoopType.Incremental)
                MaxProgress++;
        }
        protected override void OnActionCancel(){}
        protected override void OnActionEnd(){}
        protected override bool OnActionUpdate(float deltaTime)
        {
            var currentAction = _actions[_currentIndex];
            if (IsReverse && currentAction is IEasyExLoopAction exLoopAction)
            {
                exLoopAction.SetReverse(IsReverse);
                exLoopAction.SetMaxProgress(MaxProgress);
            }
            if (currentAction.Update(deltaTime))
            {
                currentAction.Complete();
                if(currentAction.LoopCount>currentAction.CurrentLoopCount)
                    return false;
                if (IsReverse)
                {
                    _currentIndex--;
                    if (_currentIndex < 0)
                        return true;
                }
                else
                {
                    _currentIndex++;
                    if (_currentIndex >= _actions.Count)
                    {
                        if (LoopType == LoopType.YoYo)
                        {
                            _currentIndex--;
                            IsReverse = true;
                            return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public static class SequenceExtension
    {
        public static Sequence Sequence(this Sequence self,Action<Sequence> set=null)
        {
            var seq = EasyFramework.Sequence.Fetch();
            set?.Invoke(seq);
            return self.Append(seq);
        }
    }
}