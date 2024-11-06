using System;
using EasyFramework.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace EasyFramework
{
    public partial class BlackBoard
    {
        [CustomPropertyDrawer(typeof(BlackBoard))]
        public class BlackBoardEditor : PropertyDrawer
        {
            BlackBoard blackBoard;
            ReorderableList list;
            static bool isExpanded;

            private void InitList(SerializedProperty property)
            {
                blackBoard = fieldInfo.GetValue(property.serializedObject.targetObject) as BlackBoard;
                var viewProperty = property.FindPropertyRelative("view");
                list = new ReorderableList(property.serializedObject, viewProperty, true,true,true,true)
                {
                    drawHeaderCallback = (Rect rect) =>
                    {
                        EditorGUI.LabelField(new Rect(rect.x+15, rect.y, rect.width * 0.25f, EditorGUIUtility.singleLineHeight), "Key");
                        EditorGUI.LabelField(new Rect(rect.x + rect.width * 0.25f+15, rect.y, rect.width * 0.15f, EditorGUIUtility.singleLineHeight), "Type");
                        EditorGUI.LabelField(new Rect(rect.x + rect.width * 0.4f+10, rect.y, rect.width * 0.6f, EditorGUIUtility.singleLineHeight), "Value");
                    },
                };
                list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var bbView = blackBoard.view[index];
                    var keyName = bbView.key;
                    var bbValue = blackBoard.GetBBValue(bbView.key);
                    var bbValueSerializedObject = ((IBBValue)bbView.serializedValue).SerializedObject;
                    bbValueSerializedObject.Update();
                    var bbValueSerializedProperty = bbValueSerializedObject.FindProperty("value");
                    
                    var typeNameOrigin = ((IBBValue) bbValue).Value?.GetType().Name.Split('.')[^1];
                    var typeName = ((IBBValue) bbValue).ValueType.Name.Split('.')[^1];
                    if (typeName == "Object" && ((IBBValue)bbValue).ValueType == typeof(object))
                        typeName = "object";
                    if (typeNameOrigin!=null && typeName != typeNameOrigin)
                        typeName = $"{typeNameOrigin}->{typeName}";
                    rect.y += 2;
                    var keyRect = new Rect(rect.x, rect.y, rect.width * 0.25f, EditorGUIUtility.singleLineHeight);
                    var typeRect = new Rect(rect.x + rect.width * 0.25f+5, rect.y, rect.width * 0.15f, EditorGUIUtility.singleLineHeight);
                    Rect valueRect = new Rect(rect.x + rect.width * 0.4f+5, rect.y, rect.width * 0.6f-5, EditorGUIUtility.singleLineHeight);

                    var newKeyName=EditorGUI.DelayedTextField(keyRect, keyName);
                    EditorGUI.LabelField(typeRect, typeName);
                    if (bbValueSerializedProperty != null)
                    {
                        if(bbValueSerializedProperty.propertyType!=SerializedPropertyType.Generic)
                            EditorGUI.PropertyField(valueRect, bbValueSerializedProperty, GUIContent.none);
                        else if (bbValueSerializedProperty.isArray || bbValueSerializedProperty.propertyType == SerializedPropertyType.Generic)
                        {
                            valueRect.x += 10;
                            valueRect.width -= 10;
                            if (!bbValueSerializedProperty.isExpanded)
                                EditorGUI.PropertyField(valueRect, bbValueSerializedProperty, new GUIContent("Value"));
                            else
                            {
                                valueRect = new Rect(rect.x, rect.y+EditorGUIUtility.singleLineHeight+EditorGUIUtility.standardVerticalSpacing, rect.width, EditorGUIUtility.singleLineHeight);
                                EditorGUI.PropertyField(valueRect, bbValueSerializedProperty, new GUIContent("Value"), true);
                            }
                        }
                    }
                    else
                        EditorGUI.LabelField(valueRect, ((IBBValue)bbValue).Value != null ? "无法序列化的类型" : "Null");
                    if (newKeyName != keyName)
                    {
                        if (!blackBoard.HasKey(newKeyName))
                        {
                            Undo.RecordObject(property.serializedObject.targetObject, "Modify BlackBoard");
                            blackBoard.view[index] = new BBValueView(){key = newKeyName, serializedValue = bbView.serializedValue};
                            blackBoard.OnAfterDeserialize();
                        }
                        else
                        {
                            Debug.LogError("[修改失败]:不允许有相同的Key");
                        }
                    }
                    bbValueSerializedObject.ApplyModifiedProperties();
                };
                list.elementHeightCallback = (int index) =>
                {
                    var bbView = blackBoard.view[index];
                    var bbValueSerializedObject = ((IBBValue)bbView.serializedValue).SerializedObject;
                    bbValueSerializedObject.Update();
                    var bbValueSerializedProperty = bbValueSerializedObject.FindProperty("value");
                    var height = EditorGUIUtility.standardVerticalSpacing;
                    if (bbValueSerializedProperty != null)
                    {
                        if (bbValueSerializedProperty.isExpanded)
                            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        return  EditorGUI.GetPropertyHeight(bbValueSerializedProperty) + height;
                    }
                    return EditorGUIUtility.singleLineHeight + height;
                };
                list.onAddDropdownCallback += (Rect rect, ReorderableList l) =>
                {
                    IMenuWindowProvider menu = ScriptableObject.CreateInstance<BBAddValueSearchMenuWindow>();
                    var screenRect = EditorGUIUtility.GUIToScreenRect(rect);
                    menu.OpenMenuWindow(screenRect.position);
                    menu.OnMenuSelectEntryAction += OnSelectedAddMenuEntry;
                };
                list.onRemoveCallback += (ReorderableList l) =>
                {
                    Undo.RecordObject(property.serializedObject.targetObject, "Remove BlackBoard Value");
                    blackBoard.view.RemoveAt(l.index);
                    blackBoard.OnAfterDeserialize();
                };
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if(list == null)
                    InitList(property);
                isExpanded = EditorGUI.Foldout(position, isExpanded, new GUIContent("BlackBoard"));
                if (isExpanded)
                    list.DoLayoutList();
            }
            void OnSelectedAddMenuEntry(Vector2 position, object entry)
            {
                Undo.RecordObject(list.serializedProperty.serializedObject.targetObject, "Add BlackBoard Value");
                var selectedType = entry as Type;
                var defaultName = Guid.NewGuid().ToString();
                blackBoard.AddByBBValueType(defaultName, selectedType);
                list.Select(list.count);
            }
        }
    }
}
