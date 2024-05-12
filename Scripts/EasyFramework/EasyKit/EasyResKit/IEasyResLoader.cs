using System.Collections;
using UnityEngine;

namespace EasyFramework.EasyResKit
{
    public interface IEasyResLoader
    {
        IEnumerator InitAsync();
        CoroutineHandle<T> LoadAssetAsync<T>(CoroutineHandle<T> handle, string path, bool instantiate) where T : Object;
        CoroutineHandle<GameObject> LoadPrefabAsync(CoroutineHandle<GameObject> handle, string path, bool instantiate);
    }
}