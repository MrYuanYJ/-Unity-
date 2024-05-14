using System;
using UnityEngine;

namespace EasyFramework
{
    public abstract class AutoMonoSingleton<T> : MonoBehaviour, ISingleton<T> where T : AutoMonoSingleton<T>
    {
        TSingleton ISingleton.Get<TSingleton>()
        {
            var instance = Instance;
            if (instance is TSingleton singleton)
                return singleton;
            throw new Exception($"[{typeof(T).Name}] can not as [{typeof(TSingleton).Name}]");
        }

        public static T Instance
        {
            get
            {
                if (!Application.isPlaying)
                    throw new Exception($"Can not create singleton [{typeof(T).Name}] in edit mode or in build mode!\n无法在编辑器或构建模式下创建单例[{typeof(T).Name}]！");
                if (ISingleton<T>.Instance != null)
                    return ISingleton<T>.Instance;

                var go = DontDestroy.Instantiate(EDontDestroy.Singleton, new GameObject(typeof(T).Name));
                ISingleton<T>.Instance = go.AddComponent<T>();
                ISingleton<T>.Instance.TryInit();
                return ISingleton<T>.Instance;
            }
        }
        public static T Get()=>ISingleton<T>.Instance;
    }
}