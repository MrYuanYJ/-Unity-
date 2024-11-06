#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using System.Text.RegularExpressions;
using EasyFramework.Editor;
using EXFunctionKit;
#endif
using System;
using System.Collections;
using UnityEngine;

namespace EasyFramework
{
    public class DropDownAttribute: PropertyAttribute
    {
        public string DataField;
        public bool FindInSerializedObject = false;

        public DropDownAttribute()
        {
        }
        public DropDownAttribute(string dataField)
        {
            DataField = dataField;
        }
        public DropDownAttribute(string dataField,bool findInSerializedObject)
        {
            DataField = dataField;
            FindInSerializedObject = findInSerializedObject;
        }
    }
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DropDownAttribute))]
    public class DropDownAttributeDrawer : PropertyDrawer
    {
        private object Value => tree[^1];
        object Parent=> tree.Count > 1? tree[^2]:tree[^1];
        List<object> tree;
        object dataFieldObj;
        object dataFieldValue;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var dropDownAttribute = (DropDownAttribute)attribute;
            var paths = property.propertyPath.Split('.');
            tree??= new List<object>();
            tree.Clear();
            FindData(property.serializedObject.targetObject, paths, 0,tree);
            IDataViewList data = null;
            
            if (!string.IsNullOrEmpty(dropDownAttribute.DataField))
            {
                if (dropDownAttribute.FindInSerializedObject)
                {
                    dataFieldObj = property.serializedObject.targetObject;
                }
                else
                {
                    Array.Resize(ref paths, paths.Length - 1);
                    dataFieldObj = ExCSharp.GetValueRecursive(property.serializedObject.targetObject, paths);
                }

                if (dataFieldObj != null)
                {
                    var bindingFlags = BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                    var dataFeldInfo = dataFieldObj.GetType().GetField(dropDownAttribute.DataField,bindingFlags);
                    if (dataFeldInfo != null)
                        dataFieldValue = dataFeldInfo.GetValue(dataFieldObj);
                    else
                    {
                        var propertyInfo = dataFieldObj.GetType().GetProperty(dropDownAttribute.DataField, bindingFlags);
                        if (propertyInfo != null)
                            dataFieldValue = propertyInfo.GetValue(dataFieldObj);
                        else
                        {
                            var methodInfo = dataFieldObj.GetType().GetMethod(dropDownAttribute.DataField, bindingFlags);
                            if (methodInfo != null)
                                dataFieldValue = methodInfo.Invoke(dataFieldObj, null);
                            else
                                throw new Exception("Can't find data field:" + dropDownAttribute.DataField);
                        }
                    }
                    data = ToDataViewList(dataFieldValue);
                }
            }
            else if(Value.GetType().IsEnum)
            {
                data = ToDataViewList(Value);
            }

            if (data == null || Parent is ValueType)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                var dropDownRect=EditorGUI.PrefixLabel(position,label);
                string text= string.Empty;
                foreach (IDataView o in data)
                {
                    if (Equals(o.Value,Value))
                    {
                        text = o.Key;
                        break;
                    }
                }
                if (EditorGUI.DropdownButton(dropDownRect,new GUIContent(text), FocusType.Keyboard))
                {
                    SearchPopupWindow.ShowPopup(dropDownRect, Value, data, view =>
                    {
                        Undo.RecordObject(property.serializedObject.targetObject, "DropdownSelection");
                        fieldInfo.SetValue(Parent, view.Value);
                    });
                }
            }
            property.serializedObject.ApplyModifiedProperties();
        }

        public IDataViewList ToDataViewList(object dataObj)
        {
            if (dataObj is Enum enumData)
                return enumData.ToDataViewList();
            else if (dataObj is IDataViewList dataViewList)
                return dataViewList;
            else if(dataObj is IEnumerable enumerable)
                return enumerable.ToDataViewList();
            else
                return null;
        }
        
        public void FindData(object target, string[] dataPaths, int index,List<object> tree)
        {
            tree??= new List<object>();
            tree.Add(target);
            var dataPath = dataPaths[index];
            var targetType = target.GetType();
            FieldInfo targetFieldInfo = null;
            
            while (targetFieldInfo == null)
            {
                targetFieldInfo = targetType.GetField(dataPath,
                    BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public |
                    BindingFlags.NonPublic);
                if (targetFieldInfo != null
                    || targetType.BaseType == null)
                    break;

                targetType = targetType.BaseType;
            }
            if (targetFieldInfo == null && dataPath == "Array")
            {
                try
                {
                    dataPath = dataPaths[++index];
                    var m = Regex.Match(dataPath, @"(.+)\[([0-9]+)*\]");
                    var arrayIndex = int.Parse(m.Groups[2].Value);
                    var arrayValue = (target as System.Collections.IList)[arrayIndex];
                    if (index < dataPaths.Length - 1)
                    {
                        FindData(arrayValue, dataPaths, ++index, tree);
                    }
                    else
                    {
                        tree.Add(arrayValue);
                        return;
                    }
                }
                catch
                {
                    Debug.Log("PropertyDrawer Exception, objType:" + target.GetType().Name + " path:" + string.Join(", ", dataPaths));
                    throw;
                }
            }
            else if (targetFieldInfo == null)
            {
                throw new Exception("Can't decode path:" + string.Join(", ", dataPaths));
            }

            var v = targetFieldInfo.GetValue(target);
            if (index < dataPaths.Length - 1)
            {
                FindData(v, dataPaths, ++index, tree);
            }

            if (!tree.Contains(v))
                tree.Add(v);
        }
    }
    #endif
}