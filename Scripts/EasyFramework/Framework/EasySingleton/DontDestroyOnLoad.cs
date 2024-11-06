using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public class DontDestroy
    {
        private static Lazy<GameObject> _normal = new Lazy<GameObject>(() =>
        {
            var go = new GameObject("——————NorMal——————");
            Object.DontDestroyOnLoad(go);
            return go;
        });

        private static Lazy<GameObject> _singleton = new Lazy<GameObject>(() =>
        {
            var go = new GameObject("——————Singleton——————");
            Object.DontDestroyOnLoad(go);
            return go;
        });

        private static Lazy<GameObject> _ui = new Lazy<GameObject>(() =>
        {
            var go = new GameObject("——————UI——————");
            Object.DontDestroyOnLoad(go);
            return go;
        });

        private static GameObject Instantiate(GameObject go, Transform parent)
        {
            go.transform.SetParent(parent);
            return go;
        }

        private static GameObject Instantiate(GameObject go, GameObject parent)
        {
            go.transform.SetParent(parent.transform);
            return go;
        }

        public static GameObject Instantiate(EDontDestroy dontDestroyType, GameObject go)
        {
            return (dontDestroyType) switch
            {
                EDontDestroy.Normal => Instantiate(go, _normal.Value),
                EDontDestroy.Singleton => Instantiate(go, _singleton.Value),
                EDontDestroy.UI => Instantiate(go, _ui.Value),
                _ => throw new Exception("未配置该类型")
            };
        }
    }
}

public enum EDontDestroy
{
    Normal,
    Singleton,
    UI,
    AudioTrack,
}