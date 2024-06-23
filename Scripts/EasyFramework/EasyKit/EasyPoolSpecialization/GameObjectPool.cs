using EasyFramework.EasyResKit;
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

        protected override void OnInit()
        {
            ForceRegister();
            EasyRes.RecycleGo.RegisterEvent(obj=>Recycle(EasyRes.GetAssetPath.InvokeFunc(obj),obj)).UnRegisterOnDispose(this);
            EasyRes.FetchGObject.RegisterFunc(Fetch).UnRegisterOnDispose(this);
            EasyRes.ReleaseGObject.RegisterEvent(ClearTarget).UnRegisterOnDispose(this);
            EasyRes.ReleaseAll.RegisterEvent(Clear).UnRegisterOnDispose(this);
        }
        
    }
}