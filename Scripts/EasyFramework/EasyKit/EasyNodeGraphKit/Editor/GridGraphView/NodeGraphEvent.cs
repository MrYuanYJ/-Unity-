using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EasyFramework.Editor
{
    public struct NodeGraphEvent
    {
        public sealed class GraphChanged: AFuncIndex<GraphChanged,GraphViewChange,GraphViewChange>{}
        public sealed class RefreshGraphView: AEventIndex<RefreshGraphView>{}
        public sealed class GetGraph: AFuncIndex<GetGraph,NodeGraph>{}
        public sealed class GetGraphView: AFuncIndex<GetGraphView,NodeGraphView>{}
        public sealed class AddNode: AEventIndex<AddNode,Type,Vector2>{}
        public sealed class RemoveNode : AEventIndex<RemoveNode, INode> { }
        public sealed class RefreshNode: AEventIndex<RefreshNode, BaseNodeView> { }
        public sealed class ExecuteNode: AEventIndex<ExecuteNode, BaseNodeView> { }
    }
}