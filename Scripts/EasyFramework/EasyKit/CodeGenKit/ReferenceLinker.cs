using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EXFunctionKit;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR

using Sirenix.Utilities;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

#endif

namespace CodeGenKit
{
    public class ReferenceLinker: MonoBehaviour,ISerializationCallbackReceiver
    {
        public Object[] referenceLink;
        public List<ReferenceLinkData> referenceLinkDataLst = new();
        private Dictionary<string, Object> referenceLinkDic = new();

        protected void OnValidate()
        {
            AutoReferenceSetting();
        }

        protected void AutoReferenceSetting()
        {
            if (referenceLink != null)
            {
                foreach (var item in referenceLink)
                {
                    if(!item)
                        continue;
                    string name = null;
                    if (item is Component component)
                        name = Regex.Replace(component.GetType().Name, @"\s", "");
                    else
                        name = Regex.Replace(item.name, @"\s", "");
                    referenceLinkDataLst.Add(new ReferenceLinkData()
                    {
                        Name = name.OnlyLettersAndNumbers(),
                        Object = item
                    });
                }
                referenceLink = null;
            }
        }
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            if(referenceLinkDic==null) return;
            
            referenceLinkDic.Clear();
            foreach (var item in referenceLinkDataLst)
            {
                referenceLinkDic.Add(item.Name, item.Object);
            }
        }

        public String[] GetAllKey()
        {
            String[] keys = new String[referenceLinkDataLst.Count];
            for (int i = 0; i < referenceLinkDataLst.Count; i++)
            {
                keys[i] = referenceLinkDataLst[i].Name;
            }

            return keys;
        }

        public Type[] GetAllType()
        {
            Type[] types = new Type[referenceLinkDataLst.Count];
            for (int i = 0; i < referenceLinkDataLst.Count; i++)
            {
                types[i] = referenceLinkDataLst[i].Object.GetType();
            }

            return types;
        }
    }
    
    [System.Serializable]
    public class ReferenceLinkData
    {
        public string Name;
        public string Type;
        public Object Object;
        
    }
    #if UNITY_EDITOR
    public class ReferenceLinkDataDrawer: OdinValueDrawer<ReferenceLinkData>
    {
        int currentIndex=0;
        List<string> options = new();
        List<string> stringOptions = new();

        protected override void Initialize()
        {
            base.Initialize();
            ReferenceLinkData data = ValueEntry.SmartValue;
            options.Clear();
            stringOptions.Clear();
            if (data.Object)
            {
                options.Add(data.Object.GetType().AssemblyQualifiedName);
                stringOptions.Add(data.Object.GetType().Name);
            }
            if(data.Object is GameObject go)
                foreach (var component in go.GetComponents<Component>())
                {
                    options.Add(component.GetType().AssemblyQualifiedName);
                    stringOptions.Add(component.GetType().Name);
                }
            
            if (data.Type != null)
            {
                currentIndex = options.IndexOf(data.Type);
            }
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            ReferenceLinkData data = ValueEntry.SmartValue;
            GUILayout.BeginHorizontal();
            if (data.Object&&data.Name.IsNullOrWhitespace())
            {
                if (data.Object is Component c)
                    data.Name = Regex.Replace(c.GetType().Name, @"\s", "");
                else
                    data.Name = Regex.Replace(data.Object.name, @"\s", "");
            }
            
            data.Name=EditorGUILayout.TextField(data.Name);

            var index = currentIndex;
            currentIndex = EditorGUILayout.Popup(currentIndex, stringOptions.ToArray());
            if (options.Count > currentIndex)
                data.Type = options[currentIndex];
            if (index != currentIndex)
                data.Name = stringOptions[currentIndex];
            Object objectField = null;
            if (!data.Object)
                objectField =EditorGUILayout.ObjectField(null,typeof(Object), true);
            else
                objectField =EditorGUILayout.ObjectField(data.Object,data.Object.GetType(), true);
            if (data.Object != objectField)
            {
                data.Object = objectField;
                Initialize();
            }
            GUILayout.EndHorizontal();
            
           // base.DrawPropertyLayout(label);
        }
    }
    #endif
}