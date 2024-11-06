using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using EXFunctionKit;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace EasyFramework
{
    [Serializable]
    public class SESProperty<T>: IESProperty<T>
    {
        [SerializeField]private T _value;
        private readonly EasyEvent<T> _propertyEvent;
        private readonly Func<T, T, bool> _equalLogic;
        private readonly Func<T, T> _clampLogic;

        public T Value
        {
            get => Get();
            set => Set(value);
        }

        public T Get() => _value;

        public void Set(T value)
        {
            if (_clampLogic != null)
                value = _clampLogic.Invoke(value);
            if (!_equalLogic(_value, value))
            {
                _value = value;
                _propertyEvent.Invoke(_value);
            }
        }
        EasyEvent<T> IESProperty<T>.PropertyEvent => _propertyEvent;
        Func<T, T, bool> IESProperty<T>.EqualLogic => _equalLogic;
        Func<T, T> IESProperty<T>.ClampLogic => _clampLogic;
        public SESProperty()
        {
            _value = default;
            _propertyEvent = new();
            _equalLogic = EqualityComparer<T>.Default.Equals;
            _clampLogic = null;
        }
        public SESProperty(T initValue, Func<T, T, bool> equalLogic = null,Func<T, T> clampLogic=null)
        {
            _value = initValue;
            _propertyEvent = new();
            _equalLogic = equalLogic ?? EqualityComparer<T>.Default.Equals;
            _clampLogic = clampLogic;
        }
        
        public IUnRegisterHandle Register(Action action)=>_propertyEvent.Register(action);
        public IUnRegisterHandle Register(Action<T> action)=>_propertyEvent.Register(action);
        public IUnRegisterHandle InvokeAndRegister(Action action)
        {
            action.Invoke();
            return _propertyEvent.Register(action);
        }
        public IUnRegisterHandle InvokeAndRegister(Action<T> action)
        {
            action.Invoke(_value);
            return _propertyEvent.Register(action);
        }

        public void UnRegister(Action action)=>_propertyEvent.UnRegister(action);
        public void UnRegister(Action<T> action)=>_propertyEvent.UnRegister(action);
        public void Clear() => _propertyEvent.Clear();
        
        public object GetBoxed() => Value;
        public void SetSilently(T value) => _value = value;
        public void SetBoxed(object value) => Value = (T) value;
        public void SetBoxedSilently(object value) => _value = (T) value;

        public void Modify<M>(M newValue,RefFunc<T,M> refFunc)
        {
            ref var refValue = ref refFunc.Invoke(ref _value);
            if (!EqualityComparer<M>.Default.Equals(refValue,newValue))
            {
                refValue = newValue;
                _propertyEvent.Invoke(_value);
            }
        }
        public void Modify<M>(M newValue,RefFunc<T,M> refFunc,Func<M, M, bool> equalLogic)
        {
           ref var refValue = ref refFunc.Invoke(ref _value);
           equalLogic ??= EqualityComparer<M>.Default.Equals;
           if (!equalLogic(refValue,newValue))
           {
               refValue = newValue;
               _propertyEvent.Invoke(_value);
           }
        }
        
        public void Modify<M>(Expression<Func<T, M>> expression, M newValue)
        {
            Value.Modify(expression, newValue, onChange:_propertyEvent.Invoke);
        }

        public void ForceNotify()
        {
            _propertyEvent.Invoke(Value);
        }
        public bool IsDispose{ get; set; }
        public IEasyEvent DisposeEvent { get; } = new EasyEvent();

        void IDisposeAble.OnDispose(bool usePool)
        {
            _value = default;
            _propertyEvent.Clear();
        }

        public static explicit operator ESProperty<T>(SESProperty<T> property)
        {
            var p = (IESProperty<T>) property;
            return new ESProperty<T>(p.Value, p.EqualLogic, p.ClampLogic);
        }
    }
    
    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(SESProperty<>))]
    internal class SerializableReactivePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var p = property.FindPropertyRelative("_value");

            EditorGUI.BeginChangeCheck();

            if (p.propertyType == SerializedPropertyType.Quaternion)
            {
                label.text += "(EulerAngles)";
                EditorGUI.PropertyField(position, p, label, true);
            }
            else
            {
                EditorGUI.PropertyField(position, p, label, true);
            }

            if (EditorGUI.EndChangeCheck())
            {
                var paths = property.propertyPath.Split('.'); // X.Y.Z...
                var attachedComponent = property.serializedObject.targetObject;

                var targetProp = (paths.Length == 1)
                    ? fieldInfo.GetValue(attachedComponent)
                    : GetValueRecursive(attachedComponent, 0, paths);
                if (targetProp == null) return;

                property.serializedObject.ApplyModifiedProperties(); // deserialize to field
                var methodInfo = targetProp.GetType().GetMethod("ForceNotify", BindingFlags.IgnoreCase | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(targetProp, null);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var p = property.FindPropertyRelative("_value");
            if (p.propertyType == SerializedPropertyType.Quaternion)
            {
                // Quaternion is Vector3(EulerAngles)
                return EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, label);
            }
            else
            {
                return EditorGUI.GetPropertyHeight(p);
            }
        }

        object GetValueRecursive(object obj, int index, string[] paths)
        {
            var path = paths[index];

            FieldInfo fldInfo = null;
            var type = obj.GetType();
            while (fldInfo == null)
            {
                // attempt to get information about the field
                fldInfo = type.GetField(path, BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (fldInfo != null ||
                    type.BaseType == null ||
                    type.BaseType.IsSubclassOf(typeof(SESProperty<>))) break;

                // if the field information is missing, it may be in the base class
                type = type.BaseType;
            }

            // If array, path = Array.data[index]
            if (fldInfo == null && path == "Array")
            {
                try
                {
                    path = paths[++index];
                    var m = Regex.Match(path, @"(.+)\[([0-9]+)*\]");
                    var arrayIndex = int.Parse(m.Groups[2].Value);
                    var arrayValue = (obj as System.Collections.IList)[arrayIndex];
                    if (index < paths.Length - 1)
                    {
                        return GetValueRecursive(arrayValue, ++index, paths);
                    }
                    else
                    {
                        return arrayValue;
                    }
                }
                catch
                {
                    Debug.Log("SerializableReactivePropertyDrawer Exception, objType:" + obj.GetType().Name + " path:" + string.Join(", ", paths));
                    throw;
                }
            }
            else if (fldInfo == null)
            {
                throw new Exception("Can't decode path:" + string.Join(", ", paths));
            }

            var v = fldInfo.GetValue(obj);
            if (index < paths.Length - 1)
            {
                return GetValueRecursive(v, ++index, paths);
            }

            return v;
        }
    }

#endif
}