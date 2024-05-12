using System;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public abstract class Singleton<T>:ISingleton<T>,IEasyLife where T : Singleton<T>, new()
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
                throw new Exception(
                    $"Can not create singleton [{typeof(T).Name}] in edit mode or in build mode!\n无法在编辑或构建模式下创建单例[{typeof(T).Name}]！");

            if (ISingleton<T>.Instance != null)
                return ISingleton<T>.Instance;

            ISingleton<T>.Instance = new T();
            ISingleton<T>.Instance.Init();
            return ISingleton<T>.Instance;
        }

        public T ForceRegister()
        {
            ISingleton<T>.Instance = (T) this;
            if (this is IEasyLife initAble)
                initAble.Init();
            return ISingleton<T>.Instance;
        }

        public bool IsInit { get; set; }
        public EasyEvent<IEasyLife> InitEvent { get; set; } = new();
        public EasyEvent<IEasyLife> StartEvent { get; set; } = new();
        public EasyEvent<IEasyLife> DisposeEvent { get; set; } = new();
        public abstract void OnInit();
        public virtual void OnStart(){}
        public abstract void OnDispose();
    }
}