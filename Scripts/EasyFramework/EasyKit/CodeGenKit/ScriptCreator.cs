#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeGenKit
{
    public class ScriptCreator : ReferenceLinker,IScriptCreator
    {
        [ShowInInspector][ValueDropdown("GetTemplateTypes",AppendNextDrawer = true)]
        private ScriptableObject _template;
        [ShowIf("@_template!=null")]
        public string nameSpace;
        [ShowIf("@_template!=null")]
        public string scriptName;
        [ShowIf("@_template!=null")][Sirenix.OdinInspector.FilePath]
        public string path =ScriptCreatorMgr.GenScriptsDir.Value;

        

        [ContextMenu("Create")]
        public void Create()
        {
            foreach (var scriptCreator in GetComponentsInChildren<ScriptCreator>())
            {
                if (scriptCreator != this)
                    scriptCreator.Create();
            }
            ((IScriptsTemplate)_template).CreateScript(this);
            AssetDatabase.Refresh();
        }
        public string NameSpace => nameSpace;
        public string ScriptName => scriptName;
        public string Path => path;
        public GameObject RootGo => gameObject;

        protected new void OnValidate()
        {
            AutoReferenceSetting();
            AutoScriptInitSetting();
        }

        private ScriptableObject lastTemplate;
        protected void AutoScriptInitSetting()
        {
            if(lastTemplate==_template) return;
            lastTemplate = _template;
            
            scriptName = Regex.Replace(gameObject.name,@"\s","");
            nameSpace = ((IScriptsTemplate)_template).Namespace;
            path = ((IScriptsTemplate)_template).Path;
        }

        private IEnumerable<ScriptableObject> GetTemplateTypes()
        {
            var q = typeof(IScriptsTemplate).Assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseClass
                .Where(x => typeof(IScriptsTemplate).IsAssignableFrom(x)); // Excludes classes not inheriting from BaseClass
            List<ScriptableObject> lst = new();
            foreach (var type in q)
            {
                lst.Add((ScriptableObject) ScriptCreatorMgr.Instance.GetTemplate(type));
            }
            return lst;
        }
    }
}
#endif