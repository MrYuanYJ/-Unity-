using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace EasyFramework.Editor
{
    public class NodeGraphWindow: EditorWindow,IEditorWindow
    {
        private GameObject _selectedGameObject;
        public EditorWindow Wnd => this;
        GameObject IEditorWindow.SelectedGameObject { get => _selectedGameObject; set => _selectedGameObject = value; }
        Dictionary<string,BaseNodeView> _nodeViewMap = new();
        Vector2 _startMousePosition;
        float _startWidth;
        public void ResetWindow()
        {
            NodeGraph = NodeGraphEvent.GetGraph.InvokeFunc();
            GraphView = NodeGraphEvent.GetGraphView.InvokeFunc();
            InspectorView = rootVisualElement.Q<VisualElement>("Inspector").Q<ScrollView>();
            var dragBar = rootVisualElement.Q<VisualElement>("DragBar");
            dragBar.AddManipulator(new Dragger());
            var inspector = rootVisualElement.Q<VisualElement>("Inspector");
            dragBar.RegisterCallback<MouseDownEvent>(evt =>
            {
                _startMousePosition = evt.mousePosition;
                _startWidth = inspector.layout.width;
            });
            dragBar.RegisterCallback<MouseMoveEvent>(evt =>
            {
                if (Event.current.type == EventType.MouseDrag)
                {
                    inspector.style.width= _startWidth + evt.mousePosition.x - _startMousePosition.x;
                }
            });
            NodeGraphEvent.AddNode.ClearEvent();
            NodeGraphEvent.RemoveNode.ClearEvent();
            NodeGraphEvent.RefreshNode.ClearEvent();
            NodeGraphEvent.GraphChanged.ClearEvent();
            NodeGraphEvent.RefreshGraphView.ClearEvent();
            NodeGraphEvent.ExecuteNode.ClearEvent();
            
            NodeGraphEvent.AddNode.RegisterEvent(AddNode);
            NodeGraphEvent.RemoveNode.RegisterEvent(RemoveNode);
            NodeGraphEvent.RefreshNode.RegisterEvent(RefreshNode);
            NodeGraphEvent.GraphChanged.RegisterFunc(OnGraphViewChanged);
            NodeGraphEvent.RefreshGraphView.RegisterEvent(RefreshGraphView);
            NodeGraphEvent.ExecuteNode.RegisterEvent(ExecuteNode);
            RefreshGraphView();
        }

        public NodeGraph NodeGraph;
        public NodeGraphView GraphView;
        public ScrollView InspectorView;

        private void OnEnable()
        {
            EditorApplication.delayCall += ResetWindow;
        }

        private void RefreshGraphView()
        {
            /*Dictionary<string,string> guidOldNewMap = new();
            foreach (var node in NodeGraph.nodes)
            {
                var inode = node as INode;
                var oldGuid = inode.Guid;
                inode.SetNewGuid();
                guidOldNewMap[oldGuid]=inode.Guid;
            }

            foreach (var node in NodeGraph.nodes)
            {
                var inode = node as INode;
                for (int i = 0; i < inode.ConnectedNodesGuid.Length; i++)
                {
                    inode.ConnectedNodesGuid[i] = guidOldNewMap[inode.ConnectedNodesGuid[i]];
                }
            }*/
            NodeGraph.InitNodeGraph();
            
            foreach (var node in GraphView.nodes)
                GraphView.RemoveElement(node);
            foreach (var edge in GraphView.edges)
                GraphView.RemoveElement(edge);
            
            OnNodeSelected(null);
            Dictionary<string, Type> nodeTypeMap = new();
            var nodeViewTypes=IEditorWindow.GetClassList(typeof(BaseNodeView));
            foreach (var nodeViewType in nodeViewTypes)
            {
                var bindAttribute = nodeViewType.GetCustomAttribute<BindAttribute>();
                if (bindAttribute == null || bindAttribute.BindType == null)
                    continue;
                
                nodeTypeMap[bindAttribute.BindType.FullName] = nodeViewType;
            }
            if (NodeGraph != null)
            {
                foreach (var node in NodeGraph.nodes)
                {
                    Type type = nodeTypeMap[node.TypeName];
                    var nodeView = CreateNodeView(type, node.NodePosition);
                    nodeView.BindNode(node);
                    _nodeViewMap[((INode) node).Guid] = nodeView;
                }
                
                //节点创建完成,创建连接线
                foreach (var node in _nodeViewMap.Keys)
                {
                    var nodeView = _nodeViewMap[node];
                    Port outputPort = nodeView.Query<Port>().Where(port => port.direction == Direction.Output);
                    
                    if(outputPort==null)
                        continue;
                    
                    foreach (var connectedNodeGuid in nodeView.Node.ConnectedNodesGuid)
                    {
                        Port inputPort = _nodeViewMap[connectedNodeGuid].inputContainer.Query<Port>().Where(port => port.direction == Direction.Input);
                        
                        if(inputPort==null)
                            continue;
                        
                        AddEdgeByPorts(outputPort, inputPort);
                    }
                }
            }
        }

        private void RefreshNode(BaseNodeView nodeView)
        {
            var node = nodeView.Node;
            List<BaseNodeView> connectedNodeViews = new();
            connectedNodeViews.Sort();
            Array.Sort(node.ConnectedNodesGuid, (x, y) =>
            {
                var posX = _nodeViewMap[x].Node.NodePosition;
                var posY = _nodeViewMap[y].Node.NodePosition;
                if(posX.y!=posY.y)
                    return _nodeViewMap[x].Node.NodePosition.y > _nodeViewMap[y].Node.NodePosition.y ? 1 : 0;
                else
                    return _nodeViewMap[x].Node.NodePosition.x < _nodeViewMap[y].Node.NodePosition.x ? 1 : 0;
            });
        }

        private BaseNodeView CreateNodeView(Type nodeViewType, Vector2 position)
        {
            if (_selectedGameObject == null)
                return null;

            BaseNodeView nodeView = Activator.CreateInstance(nodeViewType) as BaseNodeView;

            if (nodeView == null)
            {
                Debug.LogError("节点未找到对应属性的NodeView");
                return null;
            }
            nodeView.InitNodeView(this,new Rect(position, nodeView.GetPosition().size));
            nodeView.OnNodeSelected=OnNodeSelected;
            GraphView.AddElement(nodeView);
            return nodeView;
        }
        //连接两个点
        private void AddEdgeByPorts(Port _outputPort, Port _inputPort)
        {
            if (_outputPort.node == _inputPort.node)
                return;

            Edge tempEdge = new Edge()
            {
                output = _outputPort,
                input = _inputPort
            };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            GraphView.Add(tempEdge);
        }

        private void AddNode(Type nodeViewType, Vector2 position)
        {
            //获取鼠标位置
            var windowRoot = Wnd.rootVisualElement;
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, position - Wnd.position.position);
            
            var nodeView = CreateNodeView(nodeViewType, windowMousePosition);
            if (nodeView == null)
                return;
            var nodeType = nodeViewType.GetCustomAttribute<BindAttribute>().BindType;
            var node = INode.CreateInstance(nodeType);
            node.TypeName = nodeType.FullName;
            nodeView.BindNode(node);
            node.NodePosition = windowMousePosition;
            NodeGraph.AddNode(node);
            _nodeViewMap.Add(node.Guid, nodeView);
        }
        private void RemoveNode(INode node)
        {
            NodeGraph.RemoveNode(node);
            _nodeViewMap.Remove(node.Guid);
        }

        private void OnNodeSelected(BaseNodeView nodeView)
        {
            InspectorView.Clear();
            if(nodeView==null)
                return;
            SerializedObject serializedObject = new SerializedObject((Object)nodeView.Node);
            var fieldes = serializedObject.targetObject.GetType().GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
            foreach (var field in fieldes)
            {
                if(field.GetCustomAttribute<HideInInspector>()!=null
                   || field.GetCustomAttribute<NonSerializedAttribute>()!=null
                   || !field.IsPublic && field.GetCustomAttribute<SerializeField>()==null)
                    continue;
                
                var property = new PropertyField(serializedObject.FindProperty(field.Name));
                property.Bind(serializedObject);
                InspectorView.Add(property);
            }
        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            Debug.Log("OnGraphViewChanged");

            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    BaseNodeView nodeView = element as BaseNodeView;
                    if (nodeView != null)
                    {
                        NodeGraphEvent.RemoveNode.InvokeEvent(nodeView.Node);
                    }

                    Edge edge = element as Edge;
                    if (edge != null)
                    {
                        BaseNodeView parentNodeView = edge.output.node as BaseNodeView;
                        parentNodeView.OnEdgeDisconnection(edge);
                        RefreshNode(nodeView);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    BaseNodeView parentNodeView = edge.output.node as BaseNodeView;
                    parentNodeView.OnEdgeConnection(edge);
                    RefreshNode(parentNodeView);
                }
            }
            
            foreach (var n in GraphView.nodes)
            {
                var nodeView = n as BaseNodeView;
                if (nodeView != null && nodeView.Node != null)
                    nodeView.Node.NodePosition = n.GetPosition().position;
                foreach (var nodeview in _nodeViewMap.Values)
                {
                    if (nodeview.Node.ConnectedNodesGuid.Contains(nodeView.Node.Guid))
                        RefreshNode(nodeview);
                }
            }

            return graphViewChange;
        }


        private void ExecuteNode(BaseNodeView nodeView)
        {
            if(!Application.isPlaying)
                NodeGraph.InitNodeGraph();
            nodeView.Node.Execute();
        }
    }
}