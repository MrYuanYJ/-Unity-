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

        protected override void OnInit()
        {
            base.OnInit();
            ForceRegister();
            EasyRes.RecycleAsset.RegisterEvent(obj=>Recycle(EasyRes.GetAssetPath.InvokeFunc(obj),obj)).UnRegisterOnDispose(this);
            EasyRes.FetchAsset.RegisterFunc(Fetch).UnRegisterOnDispose(this);
            EasyRes.ReleaseAsset.RegisterEvent(ClearTarget).UnRegisterOnDispose(this);
            EasyRes.ReleaseAll.RegisterEvent(Clear).UnRegisterOnDispose(this);
        }
        
    }
}