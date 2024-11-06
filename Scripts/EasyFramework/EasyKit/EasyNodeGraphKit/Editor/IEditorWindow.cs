using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework.Editor
{
    public interface IEditorWindow
    { 
        EditorWindow Wnd { get; }
        
        GameObject SelectedGameObject { get;protected set; }
        void InitEditorWindow(GameObject selectedGameObject)
        {
            EditorApplication.delayCall += ResetWindow;
            SelectedGameObject = selectedGameObject;
        }
        void ResetWindow();
        public static IEditorWindow OpenWindow<T>(GameObject selectedGameObject,string title,string uxmlPath) where T : EditorWindow,IEditorWindow
        {
            T wnd = EditorWindow.GetWindow<T>();
            wnd.titleContent = new GUIContent(title);
            wnd.rootVisualElement.Clear();
            var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            treeAsset.CloneTree(wnd.rootVisualElement);
            
            InitVisualElements(wnd.rootVisualElement,wnd);
            wnd.InitEditorWindow(selectedGameObject);
            wnd.Show();
            return wnd;
        }
        public static IEditorWindow OpenWindow<T>(string title,string uxmlPath) where T : EditorWindow,IEditorWindow
        {
            return OpenWindow<T>(null,title,uxmlPath);
        }
        public static void InitVisualElements(VisualElement parent,IEditorWindow wnd)
        {
            foreach (var child in parent.Children())
            {
                InitVisualElements(child,wnd);
                if(child is IVisualElementView view)
                    view.InitView(wnd);
            }
        }
        public static List<Type> GetClassList(Type type)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var list = new List<Type>();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == "EasyFramework" ||
                    assembly.GetReferencedAssemblies()
                        .Any(targetAssembly => targetAssembly.Name == "EasyFramework"))
                {
                    var q = assembly.GetTypes()
                        .Where(x =>
                            !x.IsAbstract
                            && !x.IsGenericTypeDefinition
                            && type.IsAssignableFrom(x));
                    list.AddRange(q);
                }
            }

            return list;
        }
    }
}