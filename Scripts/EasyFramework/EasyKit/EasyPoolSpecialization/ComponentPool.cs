using System;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class ComponentPool: AMonoKeyPool<ComponentPool,Component> 
    {
        protected override AMonoValuePool<Component> CreatePool(string path)
        {
            var pool = new GameObject().AddComponent<ComponentContainer>();
            pool.gameObject.SetParent(transform);
            return pool;
        }

        private Component Fetch(Type type) => Fetch($"{type.Name}Pool");
        private void Recycle(Component obj)=> Recycle($"{obj.GetType().Name}Pool",obj);
        protected override void OnInit()
        {
            base.OnInit();
            ForceRegister();
            EasyRes.RecycleComponent.RegisterEvent(Recycle).UnRegisterOnDispose(this);
            EasyRes.FetchComponent.RegisterFunc(Fetch).UnRegisterOnDispose(this);
            EasyRes.ReleaseGObject.RegisterEvent(ClearTarget).UnRegisterOnDispose(this);
            EasyRes.ReleaseAll.RegisterEvent(Clear).UnRegisterOnDispose(this);
        }
        
    }
}