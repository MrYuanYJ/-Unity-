using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class EasyActionSingleton: AutoMonoSingleton<EasyActionSingleton>
    {
        private static long actionId = 0;
        private Dictionary<long,IEasyAction> actionDict = new Dictionary<long, IEasyAction>();
        private Queue<Action> setActionQueue = new Queue<Action>();
        

        public static long GetActionId()=> ++actionId;

        public static void AddAction(IEasyAction action)
        {
            Instance.setActionQueue.Enqueue(()=>Instance.actionDict.Add(action.ActionID, action));
        }
        public static void RemoveAction(long actionId,Action callback)
        {
            Instance.setActionQueue.Enqueue(() =>
            {
                if (Instance.actionDict.ContainsKey(actionId))
                {
                    Instance.actionDict.Remove(actionId);
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
            if (Instance.actionDict.ContainsKey(actionId))
            {
                return Instance.actionDict[actionId];
            }
            return null;
        }

        public void Update()
        {
            foreach (var action in actionDict.Values)
            {
                if (!action.IsPause && action.Update(Time.deltaTime))
                {
                    action.Complete();
                }
            }

            for (int i = setActionQueue.Count; i > 0; i--)
                setActionQueue.Dequeue()();
        }
    }
}