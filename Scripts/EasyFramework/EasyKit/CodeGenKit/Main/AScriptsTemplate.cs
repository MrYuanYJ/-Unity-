#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeGenKit
{
    public abstract class AScriptsTemplate<T> : ScriptableObject,IScriptsTemplate where T :AScriptsTemplate<T>
    {
        public string _namespace="DefaultNamespace";
        public string _path="Assets/Scripts/ScriptGens";
        public string Namespace => _namespace;
        public string Path => _path;
        public abstract Func<CreateData,string> CreateFunc { get; }
        public abstract Action<CreateData> OnCreateEnd { get; }

        public void CreateScript(ScriptCreator creator) 
        {
            var data = new CreateData()
            {
                NameSpace = creator.NameSpace,
                ScriptName = creator.ScriptName,
                Path = creator.Path,
                ScriptCreator = creator,
                RootGameObject = creator.RootGo,
                ScripTemplate = this 
            };

            var writer = File.CreateText($"{data.Path}/{data.ScriptName}.cs");
            writer.WriteLine(CreateFunc.Invoke(data));
            writer.Dispose();

            ScriptCreatorMgr.Instance.createLst.Add(data);
        }
    }

    public interface IScriptCreator
    {
        public string NameSpace { get; }
        public string ScriptName { get; }
        public string Path { get;}
        public GameObject RootGo { get; }
    }
}
#endif