using System;
using System.Reflection;
using UnityEngine;

namespace EasyFramework
{
    public abstract class Singleton<T>:ISingleton<T>,IEasyLife where T : Singleton<T>
    {
        public TSingleton Get<TSingleton>() where TSingleton : class
        {
            var instance = GetInstance();
            if (instance is TSingleton singleton)
                return singleton;
            throw new Exception($"[{typeof(T).Name}] can not as [{typeof(TSingleton).Name}]");
        }

        public static T GetInstance() => ISingleton<T>.Instance;
        public static T Instance => ISingleton<T>.Instance;

        public static T TryRegister(bool useReflection = false)
        {
            if (!Application.isPlaying)
                throw new Exception(
                    $"Can not create singleton [{typeof(T).Name}] in edit mode or in build mode!\n无法在编辑或构建模式下创建单例[{typeof(T).Name}]！");

            if (ISingleton<T>.Instance != null)
                return ISingleton<T>.Instance;

            if (!useReflection)
            {
                ISingleton<T>.Instance = Activator.CreateInstance<T>();
                ISingleton<T>.Instance.Init();
                return ISingleton<T>.Instance;
            }
            else
            {
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
        public static void Dispose(bool usePool=false)
        {
            if (ISingleton<T>.Instance == null)
                return;
            ISingleton<T>.Instance.Dispose(usePool);
            ISingleton<T>.Instance = null;
        }

        public T ForceRegister()
        {
            ISingleton<T>.Instance = (T) this;
            ISingleton<T>.Instance.Init();
            return ISingleton<T>.Instance;
        }

        public bool IsInit { get; set; }
        public bool InitDone { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();
        void IInitAble.OnInit() => OnInit();
        void IDisposeAble.OnDispose(bool usePool) => OnDispose(usePool);
        protected virtual void OnInit(){}
        protected virtual void OnInitOrActive(){}
        protected virtual void OnDispose(bool usePool){}
    }
}