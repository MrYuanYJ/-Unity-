using EasyFramework.EasySystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework
{
    public class EasyInputSystemDrawer: AFrameworkModule
    {
        public override ScriptableObject Data => EasyInputSetting.Instance;
        public override string ModuleName => "Input System";
        public override int Priority { get; }

        // public override VisualElement DrawUI()
        // {
        //     SerializedObject serializedObject = new SerializedObject(Data);
        //     var p = serializedObject.FindProperty("KeyCodeListenable");
        //     var KeyCodeListenable = new PropertyField(p);
        //     KeyCodeListenable.Bind(serializedObject);
        //     return KeyCodeListenable;
        //     //((EasyInputSetting)Data).KeyCodeListenable);
        // }
    }
}