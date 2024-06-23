using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class EasyActionSingleton: MonoSingleton<EasyActionSingleton>
    {
        private static long _actionId = 0;
        private readonly Dictionary<long,IEasyAction> _actionDict = new Dictionary<long, IEasyAction>();
        private readonly Queue<Action> _setActionQueue = new Queue<Action>();
        

        public static long GetActionId()=> ++_actionId;

        public static void AddAction(IEasyAction action)
        {
            GetInstance()._setActionQueue.Enqueue(()=>GetInstance()._actionDict.Add(action.ActionID, action));
        }
        public static void RemoveAction(long actionId,Action callback)
        {
            GetInstance()._setActionQueue.Enqueue(() =>
            {
                if (GetInstance()._actionDict.ContainsKey(actionId))
                {
                    GetInstance()._actionDict.Remove(actionId);
                    callback?.Invoke();
                }
            });
        }
        public static void RemoveAction(IEasyAction action,Action callback)
        {
            RemoveAction(action.ActionID, callback);
        }
        public static IEasyAction GetAction(long actionId)
        {
            if (GetInstance()._actionDict.ContainsKey(actionId))
            {
                return GetInstance()._actionDict[actionId];
            }
            return null;
        }

        public void Update()
        {
            foreach (var action in _actionDict.Values)
            {
                if (!action.IsPause && action.Update(Time.deltaTime))
                {
                    action.Complete();
                }
            }

            for (int i = _setActionQueue.Count; i > 0; i--)
                _setActionQueue.Dequeue()();
        }

        protected override void OnInit()
        {
            
        }

        protected override void OnDispose(bool usePool)
        {
            _actionDict.Clear();
            _setActionQueue.Clear();
        }
    }
}