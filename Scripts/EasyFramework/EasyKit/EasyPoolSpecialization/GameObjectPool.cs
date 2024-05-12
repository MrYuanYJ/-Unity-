using EasyFramework.EasyResKit;
using EasyFramework.EventKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class GameObjectPool: AMonoKeyPool<GameObjectPool,GameObject>
    {
        protected override AMonoValuePool<GameObject> CreatePool(string path)
        {
            var pool = new GameObject().AddComponent<GameObjectContainer>();
            pool.gameObject.SetParent(transform);
            return pool;
        }
    }

    public class GameObjectOnGameInitEvent : AutoClassEvent<GlobalEvent.ApplicationInit,IStructure>
    {
        protected override void Run(IStructure a)
        {
            GlobalEvent.RecycleGObject.RegisterEvent(obj=> GameObjectPool.Instance.Recycle(EasyRes.GetAssetPath(obj), obj));
        }
    }
}