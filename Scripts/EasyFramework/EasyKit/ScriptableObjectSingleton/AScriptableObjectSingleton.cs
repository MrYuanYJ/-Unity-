using System;
using System.IO;
using EasyFramework.EasyResKit;
using EasyFramework.EasyUIKit;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EasyFramework
{
    public class AScriptableObjectSingleton<T> : ScriptableObject where T : AScriptableObjectSingleton<T>
    {
        protected static T _instance ;
        
        private static readonly Lazy<string> GenKitDir = new Lazy<string>(() =>
        {
            var path = FrameworkSettings.FrameworkSOFolderPath;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        });
        private static readonly string FileName = $"{typeof(T).Name}.asset";

        public static T Instance => GetOrCreateInstance();

        public static T GetOrCreateInstance()
        {
            if (_instance == null)
            {
                var filePath = Path.Combine(GenKitDir.Value,FileName);

                if (File.Exists(filePath))
                {
                    return _instance = Resources.Load<T>(Path.Combine(FrameworkSettings.FrameworkSOFolder,typeof(T).Name));
                }

                _instance = CreateInstance<T>();
                if (!File.Exists(filePath))
                {
#if UNITY_EDITOR
                    AssetDatabase.CreateAsset(_instance, filePath);
                    AssetDatabase.SaveAssets();
                    Debug.Log($"ScriptableObject [{typeof(T).Name}] created at {filePath}.");
#else
                        throw new Exception("Can not create asset in runtime.");
#endif
                }
            }

            return _instance;
        }
    }
}