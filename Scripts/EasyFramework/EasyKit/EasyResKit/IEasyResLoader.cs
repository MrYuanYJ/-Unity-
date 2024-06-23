using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework.EasyResKit
{
    public interface IEasyResLoader
    {
        IEnumerator InitAsync();

        CoroutineHandle<Object> LoadAssetAsync(CoroutineHandle<Object> handle, Type assetType, string path, bool instantiate);
    }
}