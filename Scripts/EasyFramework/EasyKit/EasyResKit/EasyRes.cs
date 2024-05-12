using System.Collections;
using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using EXFunctionKit;
using UnityEngine;


namespace EasyFramework.EasyResKit
{
    public class EasyRes
    {
        private static bool _isInit;
        private static Dictionary<Object,string> _assetCache =new();
        public static EasyResKitSetting Setting => EasyResKitSetting.Instance;
        public static IEasyResLoader MResLoader => EasyResKitSetting.Instance.MResLoader;
        
        public static IEnumerator Init()
        { 
            yield return null;
            if (!_isInit)
            {
                _isInit = true;
                yield return MResLoader.InitAsync();
            }
        }
        
        public static Object InstantiateAsset(string path, Object asset)
        {
            var obj=Object.Instantiate(asset);
            _assetCache.TryAdd(obj,path);
            return obj;
        }
        public static T InstantiateAsset<T>(string path, T asset) where T : Object
        {
            var obj=Object.Instantiate(asset);
            _assetCache.TryAdd(obj,path);
            return obj;
        }
        public static string GetAssetPath(Object asset)
        {
            if(asset is Component component)
                asset=component.gameObject;
            return _assetCache.GetValueOrDefault(asset);
        }
        public static void ReleaseAsset<T>(T asset) where T : Object
        {
            if (asset == null) return;
            AssetPool.Get()?.Clear(GetAssetPath(asset));
            GameObjectPool.Get()?.Clear(GetAssetPath(asset));
            _assetCache.Remove(asset);
        }
        public static void ReleaseAllAssets()
        {
            AssetPool.Get()?.Clear();
            GameObjectPool.Get()?.Clear();
            _assetCache.Clear();
        }

        private static CoroutineHandle<T> LoadAssetFromPoolOrLoader<T>(CoroutineHandle<T> handle, string path, bool instantiate) where T : Object
        {
            if (instantiate && AssetPool.Instance.Fetch(path).Out(out var obj))
                EasyCoroutine.ReturnResult(handle, (T)obj);
            else
                MResLoader.LoadAssetAsync(handle, path, instantiate);
            return handle;
        }
        private static CoroutineHandle<GameObject> LoadPrefabFromPoolOrLoader(CoroutineHandle<GameObject> handle, string path, bool instantiate)
        {
            if (instantiate && GameObjectPool.Instance.Fetch(path).Out(out var obj))
                EasyCoroutine.ReturnResult(handle, obj);
            else
                MResLoader.LoadPrefabAsync(handle, path, instantiate);
            return handle;
        }

        private static CoroutineHandle<T> InitOrLoadAssetAsync<T>(string path, bool instantiate) where T : Object
        {
            var returnHandle = CoroutineHandle<T>.Fetch();
            if (!_isInit)
            {
                var handle=EasyTask.RegisterEasyCoroutine(Init);
                handle.Completed += _ =>LoadAssetFromPoolOrLoader(returnHandle, path, instantiate);
            }
            else
            {
                LoadAssetFromPoolOrLoader(returnHandle, path, instantiate);
            }

            return returnHandle;
        }
        private static CoroutineHandle<GameObject> InitOrLoadPrefabAsync(string path, bool instantiate)
        {
            var returnHandle = CoroutineHandle<GameObject>.Fetch();
            if (!_isInit)
            {
                var handle=EasyTask.RegisterEasyCoroutine(Init);
                handle.Completed += _ =>LoadPrefabFromPoolOrLoader(returnHandle, path, instantiate);
            }
            else
            {
                LoadPrefabFromPoolOrLoader(returnHandle, path, instantiate);
            }

            return returnHandle;
        }
        public static CoroutineHandle<T> LoadAssetAsync<T>(string path,bool instantiate=true) where T : UnityEngine.Object
        {
            return InitOrLoadAssetAsync<T>(path,instantiate);
        }
        public static CoroutineHandle<GameObject> LoadPrefabAsync(string path,bool instantiate=true)=> InitOrLoadPrefabAsync(path,instantiate);
        public static CoroutineHandle<T> LoadPrefabAsync<T>(string path,bool instantiate=true) where T : Component
        {
            var handle = CoroutineHandle<T>.Fetch();
            var waitHandle = InitOrLoadPrefabAsync(path,instantiate);
            waitHandle.Completed += wait => EasyCoroutine.ReturnResult(handle, wait.Result.GetComponent<T>());
            return handle;
        }
        
        public static CoroutineHandle<T> LoadPrefabAsync<T>(bool instantiate=true) where T : Component
        {
            var handle = CoroutineHandle<T>.Fetch();
            var waitHandle = InitOrLoadPrefabAsync(GetAssetPath<T>(),instantiate);
            waitHandle.Completed += wait => EasyCoroutine.ReturnResult(handle, wait.Result.GetComponent<T>());
            return handle;
        }

        private static string GetAssetPath<T>() where T : Object
        {
            #if UNITY_EDITOR
            if (Setting.loadMode == LoadMode.EditorAssetBundle)
                return UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(EasyResKitSetting.Instance.defaultAssetBundleName, typeof(T).Name)[0];
            else
                return typeof(T).Name;
            #else
                return typeof(T).Name;
            #endif
        }
    }
}