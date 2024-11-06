using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework
{
    public interface IFrameworkModule
    {
        ScriptableObject Data { get; }
        string ModuleName { get; }
        int Priority { get; }
        SerializedObject SerializedData { get; }
        VisualElement DrawUI();
    }
    public abstract class AFrameworkModule:IFrameworkModule
    {
        public abstract ScriptableObject Data { get;}
        public abstract string ModuleName { get;}
        public abstract int Priority { get; }

        public SerializedObject SerializedData
        {
            get
            {
                if (_serializedData == null)
                    _serializedData = new SerializedObject(Data);
                return _serializedData;
            }
        }
        private SerializedObject _serializedData;
        public virtual VisualElement DrawUI() => new VisualElement();
    }
}