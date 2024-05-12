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
            if (EasyRes.Setting.loadMode != LoadMode.EditorAssetBundle)
            {
                //初始化接入的资源管理系统
                //var handle = CoroutineHandle.Fetch();
                //Addressables.InitializeAsync().Completed+=_=> ((ICoroutineHandle)handle).Complete();
            }

            yield break;
        }

        private void LoadAssetFromExternalSystem<T>(string path, Action<T> callback)
        {
            //TODO: 待接入资源管理系统后，在运行时加载资源
            throw new Exception("未接入资源管理系统，无法加载资源！！");
            //Addressables.LoadAssetAsync<T>(path).Completed += x => callback(x.Result);
        }

        public CoroutineHandle<T> LoadAssetAsync<T>(CoroutineHandle<T> handle, string path, bool instantiate) where T : Object
        {
            if (EasyRes.Setting.loadMode == LoadMode.EditorAssetBundle)
                EasyTask.RegisterResultCoroutine(LoadAsset<T>, handle, path, instantiate);
            else
            {
                LoadAssetFromExternalSystem<T>(path,
                    x => { EasyCoroutine.ReturnResult(handle, instantiate ? EasyRes.InstantiateAsset(path, x) : x); });
            }

            return handle;
        }

        public CoroutineHandle<GameObject> LoadPrefabAsync(CoroutineHandle<GameObject> handle, string path, bool instantiate)
        {
            if (EasyRes.Setting.loadMode == LoadMode.EditorAssetBundle)
                EasyTask.RegisterResultCoroutine(LoadAsset, handle, path, instantiate);
            else
            {
                LoadAssetFromExternalSystem<GameObject>(path,
                    x =>
                    {
                        EasyCoroutine.ReturnResult(handle, instantiate ? EasyRes.InstantiateAsset(path, x) : x);
                    });
            }

            return handle;
        }

        private IEnumerator LoadAsset<T>(CoroutineHandle<T> handle, string path, bool instantiate) where T : Object
        {
#if UNITY_EDITOR
            T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            EasyCoroutine.ReturnResult(handle, instantiate ? EasyRes.InstantiateAsset(path, asset) : asset);
            yield return null;
#else
            throw new System.Exception("Runtime load asset is not supported in editor mode.");
#endif
        }
    }
}