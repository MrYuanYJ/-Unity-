using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework.Editor
{
    public class NodeGraphView : GraphView,IVisualElementView<NodeSearchMenuWindow>
    {
        private IEditorWindow _window;
        IEditorWindow IVisualElementView.Window { get => _window; set => _window = value; }
        IMenuWindowProvider IVisualElementView<NodeSearchMenuWindow>.MenuWindowProvider { get; set; }
        

        public new class UxmlFactory : UxmlFactory<NodeGraphView, GraphView.UxmlTraits>
        {
        }
        
        public NodeGraphView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/Scripts/EasyFramework/EasyKit/EasyNodeGraphKit/Editor/GridGraphView/GridGraphViewStyle.uss");
            styleSheets.Add(styleSheet);
            var visualElementView = (IVisualElementView<NodeSearchMenuWindow>)this;
            visualElementView.CreateMenuWindowProvider();
            
            graphViewChanged=NodeGraphEvent.GraphChanged.InvokeFunc;
            NodeGraphEvent.GetGraphView.ClearEvent();
            NodeGraphEvent.GetGraphView.RegisterFunc(GetNodeGraphView);
            
            nodeCreationRequest += context=>visualElementView.OpenMenuRequest(context.screenMousePosition);
            visualElementView.MenuWindowProvider.OnMenuSelectEntryAction += OnMenuSelectEntry;
        }

        private NodeGraphView GetNodeGraphView() => this;
        private void OnMenuSelectEntry(Vector2 windowMousePosition,object data)
        {
            var type = (Type)data;
            var graphMousePosition = contentViewContainer.WorldToLocal(windowMousePosition);
            NodeGraphEvent.AddNode.InvokeEvent(type,graphMousePosition);
        }
        

        //判断每个点是否可以相连
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }
    }
}