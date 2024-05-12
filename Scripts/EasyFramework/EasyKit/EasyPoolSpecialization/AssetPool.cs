using EasyFramework.EasyResKit;
using EasyFramework.EventKit;
using EXFunctionKit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public class AssetPool: AMonoKeyPool<AssetPool,Object>
    {
        protected override AMonoValuePool<Object> CreatePool(string path)
        {
            var pool = new GameObject().AddComponent<AssetContainer>();
            pool.gameObject.SetParent(transform);
            return pool;
        }
    }
    public class AssetPoolOnGameInitEvent : AutoClassEvent<GlobalEvent.ApplicationInit,IStructure>
    {
        protected override void Run(IStructure a)
        {
            GlobalEvent.RecycleAsset.RegisterEvent(obj=>AssetPool.Instance.Recycle(EasyRes.GetAssetPath(obj),obj));
        }
    }
}