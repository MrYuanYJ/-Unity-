using System;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public abstract class MonoSingleton<T>: MonoBehaviour,ISingleton<T>,IEasyLife where T : MonoSingleton<T>
    {
        public TSingleton Get<TSingleton>() where TSingleton : class
        {
            var instance = Instance;
            if (instance is TSingleton singleton)
                return singleton;
            throw new Exception($"[{typeof(T).Name}] can not as [{typeof(TSingleton).Name}]");
        }
        public static T Instance => ISingleton<T>.Instance;

        public static T Register()
        {
            if (!Application.isPlaying)
                throw new Exception($"Can not create singleton [{typeof(T).Name}] in edit mode or in build mode!\n无法在编辑器或构建模式下创建单例[{typeof(T).Name}]！");
            if (ISingleton<T>.Instance != null)
                return ISingleton<T>.Instance;

            var go = DontDestroy.Instantiate(EDontDestroy.Singleton, new GameObject(typeof(T).Name));
            ISingleton<T>.Instance = go.AddComponent<T>();
            ISingleton<T>.Instance.Init();
            return ISingleton<T>.Instance;
        }

        public T ForceRegister()
        {
            ISingleton<T>.Instance = (T) this;
            ISingleton<T>.Instance.Init();
            return ISingleton<T>.Instance;
        }
        
        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent StartEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();
        public abstract void OnInit();
        public virtual void OnStart(){}
        public abstract void OnDispose();
    }
}