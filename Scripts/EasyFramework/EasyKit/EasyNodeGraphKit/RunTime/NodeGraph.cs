using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EasyFramework.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    public class NodeGraph: MonoBehaviour
    {
        //[HideInInspector] 
        public List<BaseNode> nodes;
        public Dictionary<string, BaseNode> NodeDict = new();

#if UNITY_EDITOR
        
        [Button]
        public void OpenNodeGraph()
        {
            NodeGraphEvent.GetGraph.ClearEvent();

            NodeGraphEvent.GetGraph.RegisterFunc(GetSelectedGraphNodes);
            
            IEditorWindow.OpenWindow<NodeGraphWindow>(gameObject,"EasyNodeGraphSample","Assets/Scripts/EasyFramework/EasyKit/EasyNodeGraphKit/EasyNodeGraphView.uxml");
        }
        private NodeGraph GetSelectedGraphNodes()=> this;
#endif

        public void AddNode(INode node)
        {
            nodes.Add((BaseNode)node);
        }

        public void RemoveNode(INode node)
        {
            nodes.Remove((BaseNode)node);
        }

        public void InitNodeGraph()
        {
            NodeDict.Clear();
            foreach (var node in nodes)
            {
                NodeDict[((INode)node).Guid] = node;
            }

            foreach (var node in nodes)
            {
               node.InitNode(this);
            }
        }

        private void OnEnable()
        {
            InitNodeGraph();
        }
    }
}