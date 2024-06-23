using System;
using System.Collections;
using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using EXFunctionKit;
using UnityEngine;
using Object = UnityEngine.Object;


namespace EasyFramework.EasyResKit
{
    public class EasyResSystem: ASystem
    {
        private static bool _isInit;
        private static readonly Dictionary<Object,string> AssetCache =new();
        public static EasyResKitSetting Setting => EasyResKitSetting.Instance;
        public static IEasyResLoader MResLoader => EasyResKitSetting.Instance.MResLoader;


        protected override void OnInit()
        {
            EasyRes.GetAssetPath.RegisterFunc(GetAssetPath).UnRegisterOnDispose(this);
            EasyRes.LoadAssetAsync.RegisterFunc(LoadAssetAsync).UnRegisterOnDispose(this);
            EasyRes.LoadPrefabByTypeAsync.RegisterFunc(LoadPrefabAsync).UnRegisterOnDispose(this);
            EasyRes.ReleaseAll.RegisterEvent(AssetCache.Clear).UnRegisterOnDispose(this);
        }

        protected override void OnDispose(bool usePool)
        {
            ReleaseAllAssets();
        }

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
            AssetCache.TryAdd(obj,path);
            return obj;
        }
        public static T InstantiateAsset<T>(string path, T asset) where T : Object
        {
            var obj=Object.Instantiate(asset);
            AssetCache.TryAdd(obj,path);
            return obj;
        }
        public static string GetAssetPath(Object asset)
        {
            if(asset is Component component)
                asset=component.gameObject;
            return AssetCache.GetValueOrDefault(asset);
        }
        public static void ReleaseAsset<T>(T asset) where T : Object
        {
            if (asset == null) return;
            if (asset is Component or GameObject)
            {
                GameObject go=asset.TryGetGameObject();
                EasyRes.ReleaseGObject.InvokeEvent(GetAssetPath(go));
            }
            else
                EasyRes.ReleaseAsset.InvokeEvent(GetAssetPath(asset));
            AssetCache.Remove(asset);
        }
        public static void ReleaseAllAssets()
        {
            EasyRes.ReleaseAll.InvokeEvent();
        }

        private static CoroutineHandle<Object> LoadAssetFromPoolOrLoader(CoroutineHandle<Object> handle,Type assetType, string path, bool instantiate)
        {
            if (assetType == typeof(GameObject) && instantiate && EasyRes.FetchGObject.InvokeFunc(path).Out(out var go))
                EasyCoroutine.ReturnResult(handle, go);
            else if (instantiate && EasyRes.FetchAsset.InvokeFunc(path).Out(out var obj))
                EasyCoroutine.ReturnResult(handle, obj);
            else
                MResLoader.LoadAssetAsync(handle,assetType, path, instantiate);
            return handle;
        }

        private static CoroutineHandle<Object> InitOrLoadAssetAsync(Type assetType,string path, bool instantiate)
        {
            var returnHandle = CoroutineHandle<Object>.Fetch();
            if (!_isInit)
            {
                var handle=EasyTask.RegisterEasyCoroutine(Init);
                handle.Completed += _ =>LoadAssetFromPoolOrLoader(returnHandle,assetType, path, instantiate);
            }
            else
            {
                LoadAssetFromPoolOrLoader(returnHandle,assetType, path, instantiate);
            }

            return returnHandle;
        }
        public static CoroutineHandle<Object> LoadAssetAsync(Type assetType,string path,bool instantiate=true)
        {
            return InitOrLoadAssetAsync(assetType,path,instantiate);
        }
        public static CoroutineHandle<T> LoadAssetAsync<T>(string path,bool instantiate=true) where T : Object
        {
            var handle=CoroutineHandle<T>.Fetch();
            LoadAssetAsync(typeof(T),path,instantiate).OnCompleted(h=>EasyCoroutine.ReturnResult(handle,(T)h.Result));
            return handle;
        }
        public static CoroutineHandle<GameObject> LoadPrefabAsync(string path, bool instantiate = true)
            => LoadAssetAsync<GameObject>(path, instantiate);
        public static CoroutineHandle<GameObject> LoadPrefabAsync(Type type, bool instantiate = true)
            => LoadAssetAsync<GameObject>(GetAssetPathByType(type), instantiate);
        public static CoroutineHandle<T> LoadPrefabAsync<T>(string path,bool instantiate=true) where T : Component
        {
            var handle=CoroutineHandle<T>.Fetch();
            LoadAssetAsync(typeof(GameObject),path,instantiate).OnCompleted(h=>EasyCoroutine.ReturnResult(handle,h.Result.TryGetComponent<T>()));
            return handle;
        }
        public static CoroutineHandle<T> LoadPrefabAsync<T>(bool instantiate=true) where T : Component
        {
            var handle=CoroutineHandle<T>.Fetch();
            LoadAssetAsync(typeof(GameObject),GetAssetPathByType(typeof(T)),instantiate).OnCompleted(h=>EasyCoroutine.ReturnResult(handle,h.Result.TryGetComponent<T>()));
            return handle;
        }

        private static string GetAssetPathByType(Type type)
        {
            #if UNITY_EDITOR
            if (Setting.loadMode == LoadMode.EditorAssetBundle)
                return UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(EasyResKitSetting.Instance.defaultAssetBundleName, type.Name)[0];
            else
                return type.Name;
            #else
                return type.Name;
            #endif
        }
    }
}