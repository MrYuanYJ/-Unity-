#if UNITY_EDITOR

using System;
using System.Linq;
using System.Reflection;
using EXFunctionKit;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace CodeGenKit
{
    [System.Serializable]
    public class CreateData
    {
        public string NameSpace;
        public string ScriptName;
        public string Path;
        public GameObject RootGameObject;
        public ScriptCreator ScriptCreator;
        public ScriptableObject ScripTemplate;
        
        public Type GetScriptType()
        {
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(Path + "/" + ScriptName + ".cs");
            var type = script.GetClass();
            
            return type;
        }

        public void BindLinkData(Component component)
        {
            foreach (var item in ScriptCreator.referenceLinkDataLst)
            {
                if (typeof(IScriptCreator).IsAssignableFrom(Type.GetType(item.Type)))
                {
                    var targetScriptCreator = item.Object.TryGetComponent<ScriptCreator>();
                    component.GetType().GetField(item.Name).SetValue(component,
                        item.Object.TryGetComponent(ScriptCreatorMgr.Instance.createLst.First(data =>
                                data.NameSpace == targetScriptCreator.nameSpace &&
                                data.ScriptName == targetScriptCreator.scriptName).GetScriptType()));
                }
                else
                {
                    foreach (var referenceLinkData in ScriptCreator.referenceLinkDataLst)
                    {
                        var type = Type.GetType(referenceLinkData.Type);
                        if (type.IsSubclassOf(typeof(Component)))
                        {
                            if (referenceLinkData.Object is GameObject go)
                                referenceLinkData.Object = go.GetComponent(type);
                            else if (referenceLinkData.Object is Component comp)
                                referenceLinkData.Object = comp.GetComponent(type);
                        }
                        else if (type == typeof(GameObject))
                        {
                            if (referenceLinkData.Object is Component comp)
                                referenceLinkData.Object = comp.gameObject;
                            else if (referenceLinkData.Object is GameObject go)
                                referenceLinkData.Object = go;
                        }
                    }
                    component.GetType().GetField(item.Name).SetValue(component, item.Object);
                }
            }
        }
        public void CreateFiled(IFieldContainer fieldContainer)
        {
            foreach (var item in ScriptCreator.referenceLinkDataLst)
            {
                var type = Type.GetType(item.Type);
                if (typeof(IScriptCreator).IsAssignableFrom(type))
                    fieldContainer.Field((IScriptCreator)item.Object.TryGetComponent(typeof(IScriptCreator)),item, code =>
                        code.PublicType(EPublicType.Public));
                else
                    fieldContainer.Field(type, item.Name, code =>
                        code.PublicType(EPublicType.Public));
            }
        }
    }
}

#endif