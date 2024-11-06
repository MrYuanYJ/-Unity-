using UnityEditor;
using UnityEngine;

namespace EasyFramework.Editor
{
    public interface IVisualElementView
    {
        IEditorWindow Window { get; protected set; }
        void InitView(IEditorWindow window)
        {
            Window = window;
            OnInitView();
        }

        void OnInitView(){}
        void OpenMenuRequest(Vector2 screenMousePosition);
    }
    public interface IVisualElementView<T>: IVisualElementView where T : ScriptableObject,IMenuWindowProvider
    {
        IMenuWindowProvider MenuWindowProvider { get; protected set; }
        

        void IVisualElementView.InitView(IEditorWindow editorWindow)
        {
            Window=editorWindow;
            CreateMenuWindowProvider();
            OnInitView();
        }

        void CreateMenuWindowProvider()
        {
            if(MenuWindowProvider != null)
                return;
            MenuWindowProvider = ScriptableObject.CreateInstance<T>();
        }
        /// <summary> 需要被主动调用的函数，打开菜单窗口 </summary>
        void IVisualElementView.OpenMenuRequest(Vector2 screenMousePosition)
        {
            MenuWindowProvider.OpenMenuWindow(screenMousePosition);
        }
    }
}