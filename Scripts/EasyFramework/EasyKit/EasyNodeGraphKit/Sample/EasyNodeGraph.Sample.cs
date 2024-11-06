using UnityEngine;
#if UNITY_EDITOR
using EasyFramework.Editor;
using UnityEditor;
#endif
namespace EasyFramework.Sample
{
    public class EasyNodeGraph_Sample : MonoBehaviour
    {
#if UNITY_EDITOR

        [MenuItem("GameObject/EasyNodeGraoh_Sample", false, priority = 0)]
        private static void CreateFlowChartInScene()
        {
            var gameObject = new GameObject(typeof(EasyNodeGraph_Sample).Name);

            Undo.RegisterCreatedObjectUndo(gameObject, "Create");

            gameObject.AddComponent<EasyNodeGraph_Sample>();

            if (Selection.activeGameObject != null)
            {
                gameObject.transform.parent = Selection.activeGameObject.transform;

            }

            Selection.activeGameObject = gameObject;
        }
        
        
        [CustomEditor(typeof(EasyNodeGraph_Sample))]
        public class EasyNodeGraph_SampleEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                
                if (GUILayout.Button("Open"))
                {
                    EasyNodeGraph_SampleEditorWindow._selectedGameObject = (target as MonoBehaviour)?.gameObject;
                    EasyNodeGraph_SampleEditorWindow.OpenWindow();
                }
            }
        }
        public class EasyNodeGraph_SampleEditorWindow : EditorWindow,IEditorWindow
        {
            public static void OpenWindow()
            {
                /*EasyNodeGraph_SampleEditorWindow wnd = GetWindow<EasyNodeGraph_SampleEditorWindow>();
                wnd.titleContent = new GUIContent("EasyNodeGraoh_Sample");
                var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/Scripts/EasyFramework/EasyKit/EasyNodeGraphKit/EasyNodeGraphView.uxml");
                var test = treeAsset.Instantiate();
                test.style.flexGrow = 1;
                wnd.rootVisualElement.Add(test);*/
                var wnd=IEditorWindow.OpenWindow<NodeGraphWindow>(_selectedGameObject,"EasyNodeGraoh_Sample","Assets/Scripts/EasyFramework/EasyKit/EasyNodeGraphKit/EasyNodeGraphView.uxml");
            }

            public static GameObject _selectedGameObject;

            public EditorWindow Wnd => this;
            GameObject IEditorWindow.SelectedGameObject { get => _selectedGameObject; set => _selectedGameObject = value; }
            public void ResetWindow()
            {
                
            }
        }
#endif
    }
}