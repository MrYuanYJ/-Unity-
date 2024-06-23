using System;
using System.Collections;
using EasyFramework.EasyTaskKit;
using UnityEngine;
//using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace EasyFramework.EasyResKit
{
    public class DefaultResLoader : IEasyResLoader
    {
        public IEnumerator InitAsync()
        {
#if UNITY_EDITOR

#else
            EasyReskit.Setting.loadMode = LoadMode.Othres;

#endif
            if (EasyResSystem.Setting.loadMode != LoadMode.EditorAssetBundle)
            {
                //初始化接入的资源管理系统
                //var handle = CoroutineHandle.Fetch();
                //Addressables.InitializeAsync().Completed+=_=> ((ICoroutineHandle)handle).Complete();
            }

            yield break;
        }

        private void LoadAssetFromExternalSystem(Type assetType,string path, Action<Object> callback)
        {
            //TODO: 待接入资源管理系统后，在运行时加载资源
            throw new Exception("未接入资源管理系统，无法加载资源！！");
            //Addressables.LoadAssetAsync<T>(path).Completed += x => callback(x.Result);
        }

        public CoroutineHandle<Object> LoadAssetAsync(CoroutineHandle<Object> handle,Type assetType, string path, bool instantiate)
        {
            if (EasyResSystem.Setting.loadMode == LoadMode.EditorAssetBundle)
                EasyTask.RegisterResultCoroutine(LoadAsset, handle,assetType, path, instantiate);
            else
            {
                LoadAssetFromExternalSystem(assetType,path,
                    x => { EasyCoroutine.ReturnResult(handle, instantiate ? EasyResSystem.InstantiateAsset(path, x) : x); });
            }

            return handle;
        }
        

        private IEnumerator LoadAsset(CoroutineHandle<Object> handle,Type assetType, string path, bool instantiate)
        {
#if UNITY_EDITOR
            Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(path,assetType);
            EasyCoroutine.ReturnResult(handle, instantiate ? EasyResSystem.InstantiateAsset(path, asset) : asset);
            yield return null;
#else
            throw new System.Exception("Runtime load asset is not supported in editor mode.");
#endif
        }
    }
}