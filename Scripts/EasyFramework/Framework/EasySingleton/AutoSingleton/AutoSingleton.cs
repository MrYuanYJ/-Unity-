using System;
using System.Reflection;
using UnityEngine;

namespace EasyFramework
{
    public abstract class AutoSingleton<T> : ISingleton<T> where T : AutoSingleton<T>
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
                    throw new Exception(
                        $"Can not create singleton [{typeof(T).Name}] in edit mode or in build mode!\n无法在编辑或构建模式下创建单例[{typeof(T).Name}]！");

                if (ISingleton<T>.Instance != null)
                    return ISingleton<T>.Instance;

                var constructors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
                {
                    ISingleton<T>.Instance = (T) constructors[0].Invoke(null);
                    ISingleton<T>.Instance.TryInit();
                    return ISingleton<T>.Instance;
                }

                throw new Exception(
                    $"The singleton [{typeof(T).Name}] must have exactly one constructor and and it must be a non-public parameterless constructor.\n单例类[{typeof(T).Name}]必须有且仅有一个构造函数，并且必须是非公开的无参构造函数。");
            }
        }
        public static T Get()=>ISingleton<T>.Instance;
    }
}