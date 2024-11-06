using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace EasyFramework.Editor
{
    public abstract class BaseNodeView: UnityEditor.Experimental.GraphView.Node
    {
        /// <summary>
        /// 点击该节点时被调用的事件，比如转发该节点信息到Inspector中显示
        /// </summary>
        public Action<BaseNodeView> OnNodeSelected;
        protected virtual Orientation PortOrientation => Orientation.Horizontal;
        public IEditorWindow Window { get; set; }
        public abstract INode Node { get; set; }

        public void InitNodeView(IEditorWindow window,Rect rect)
        {
            SetPosition(rect);
            Window = window;
        }

        public void BindNode(INode node)
        {
            Node = node;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            evt.menu.AppendAction("Execute", action =>
            {
                NodeGraphEvent.ExecuteNode.InvokeEvent(this);
            });
        }

        // 为节点n创建input port或者output port
        // Direction: 是一个简单的枚举，分为Input和Output两种
        public static Port CreatePort(BaseNodeView node, Direction portDir, Port.Capacity capacity = Port.Capacity.Single)
        {
            // Orientation也是个简单的枚举，分为Horizontal和Vertical两种，port的数据类型是bool
            return node.InstantiatePort(node.PortOrientation, portDir, capacity, typeof(bool));
        }
        public Port CreatePort(Direction portDir, Port.Capacity capacity = Port.Capacity.Single)
        {
            return CreatePort(this, portDir, capacity);
        }
        
        //告诉Inspector去绘制该节点
        public override void OnSelected()
        {
            base.OnSelected();
            Debug.Log($"{this.name}节点被点击");
            OnNodeSelected?.Invoke(this);
        }
        public virtual void OnEdgeConnection(Edge edge)
        {
            // 连接线条的处理
            BaseNodeView targetView= edge.input.node as BaseNodeView;
            Node.AddConnection(targetView.Node.Guid);
        }
        public virtual void OnEdgeDisconnection(Edge edge)
        {
            // 断开线条的处理
            BaseNodeView targetView= edge.input.node as BaseNodeView;
            Node.RemoveConnection(targetView.Node.Guid);
        }
    }
}