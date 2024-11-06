using System.Collections.Generic;

namespace EasyFramework
{
    public class CollisionInfo
    {
        public Dictionary<IEntity,HashSet<CollisionBoxListener>> OtherCollision2DBoxListeners=new();
        public HashSet<CollisionBoxListener> SelfCollision2DBoxListeners=new();

        public CollisionBoxListener GetOtherMaxWeightListener(IEntity entity)
        {
            CollisionBoxListener maxWeight=null;
            foreach (var listener in OtherCollision2DBoxListeners[entity])
            {
                if (!maxWeight||listener.data.weight > maxWeight.data.weight)
                {
                    maxWeight = listener;
                    if(maxWeight.data.weight >= 1)
                        return maxWeight;
                }
            }
            return maxWeight;
        }
        public CollisionBoxListener GetOtherMaxWeightListener()
        {
            CollisionBoxListener maxWeight=null;
            foreach (var listeners in OtherCollision2DBoxListeners.Values)
            {
                foreach (var listener in listeners)
                {
                    if (!maxWeight||listener.data.weight > maxWeight.data.weight)
                    {
                        maxWeight = listener;
                        if(maxWeight.data.weight >= 1)
                            return maxWeight;
                    }
                }
            }
            return maxWeight;
        }
        public CollisionBoxListener GetSelfMaxWeightListener()
        {
            CollisionBoxListener maxWeight=null;
            foreach (var listener in SelfCollision2DBoxListeners)
            {
                if (!maxWeight||listener.data.weight > maxWeight.data.weight)
                {
                    maxWeight = listener;
                    if(maxWeight.data.weight >= 1)
                        return maxWeight;
                }
            }
            return maxWeight;
        }

        public void AddSelfCollision2DBoxListener(CollisionBoxListener listener)
        {
            SelfCollision2DBoxListeners.Add(listener);
        }
        public void AddOtherCollision2DBoxListener(CollisionBoxListener listener)
        {
            if (!OtherCollision2DBoxListeners.TryGetValue(listener.belongEntity, out var listeners))
            {
                listeners = new();
                OtherCollision2DBoxListeners.Add(listener.belongEntity, listeners);
            }
            listeners.Add(listener);
        }


        public void RemoveSelfCollision2DBoxListener(CollisionBoxListener listener)
        {
            SelfCollision2DBoxListeners.Remove(listener);
        }
        public void RemoveOtherCollision2DBoxListener(CollisionBoxListener listener)
        {
            if (OtherCollision2DBoxListeners.TryGetValue(listener.belongEntity, out var listeners))
            {
                listeners.Remove(listener);
            }
        }
    }
}