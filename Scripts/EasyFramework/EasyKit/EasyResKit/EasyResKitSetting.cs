using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework.EasyResKit
{
    public enum LoadMode
    {
        EditorAssetBundle,
        Othres,
    }
    public class EasyResKitSetting : AScriptableObjectSingleton<EasyResKitSetting>
    {
        public readonly DefaultResLoader MResLoader = new();
        public LoadMode loadMode = LoadMode.EditorAssetBundle;
        public string defaultAssetBundleDirectory = "Assets/AssetBundles";
        public string defaultAssetBundleName = "easybundle";
    }
}