#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace CodeGenKit
{
    public class ScriptCreatorMgr : ScriptableObject
    {
        private static ScriptCreatorMgr _instance;

        public static readonly Lazy<string> GenKitDir = new Lazy<string>(() =>
        {
            if (!Directory.Exists("Assets/Data/CodeGenKit"))
                Directory.CreateDirectory("Assets/Data/CodeGenKit");
            return "Assets/Data/CodeGenKit";
        });

        public static readonly Lazy<string> GenScriptsDir = new Lazy<string>(() =>
        {
            if (_instance)
            {
                if (!Directory.Exists(Instance.defaultScriptsDir))
                    Directory.CreateDirectory(Instance.defaultScriptsDir);
                return Instance.defaultScriptsDir;
            }
            else
            {
                var path = "Assets/Scripts/ScriptGens";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        });

        private static readonly string FileName = $"ScriptCreatorMgr.asset";
        public List<CreateData> createLst = new();

        [Sirenix.OdinInspector.FilePath(ParentFolder = "Assets/Scripts/ScriptGens")]
        public string defaultScriptsDir = "Assets/Scripts/ScriptGens";

        public string defaultNamespace = "DefaultNamespace";

        public static ScriptCreatorMgr Instance
        {
            get
            {
                if (_instance)
                    return _instance;
                else
                {
                    var filePath = Path.Combine(GenKitDir.Value, FileName);

                    if (File.Exists(filePath))
                        return _instance = AssetDatabase.LoadAssetAtPath<ScriptCreatorMgr>(filePath);

                    _instance = CreateInstance<ScriptCreatorMgr>();
                    if (!File.Exists(filePath))
                        AssetDatabase.CreateAsset(_instance, filePath);
                    return _instance;
                }
            }
        }

        [DidReloadScripts]
        public static void AfterScriptReload()
        {
            foreach (var createData in Instance.createLst)
                createData.RootGameObject.AddComponent(createData.GetScriptType());

            foreach (var createData in Instance.createLst)
                createData.BindLinkData(createData.RootGameObject.GetComponent(createData.GetScriptType()));
            foreach (var createData in Instance.createLst)
            {
                DestroyImmediate(createData.ScriptCreator);
                ((IScriptsTemplate) createData.ScripTemplate).OnCreateEnd(createData);
            }

            Instance.createLst.Clear();
        }

        public IScriptsTemplate GetTemplate(Type type)
        {
            var template = AssetDatabase.LoadAssetAtPath<ScriptableObject>(Path.Combine(GenKitDir.Value,$"{type.Name}.asset"));
   
            if (null!=template)
                return (IScriptsTemplate)template;
            template =  CreateInstance(type);
            AssetDatabase.CreateAsset(template,Path.Combine(GenKitDir.Value,$"{type.Name}.asset"));
            AssetDatabase.SaveAssets();
            
            return (IScriptsTemplate)template;
        }
    }
}
#endif