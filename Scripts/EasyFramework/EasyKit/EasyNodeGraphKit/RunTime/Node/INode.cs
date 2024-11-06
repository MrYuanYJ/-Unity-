using System;
using System.Threading.Tasks;
using EasyFramework.Editor;
using UnityEngine;

namespace EasyFramework
{
    public interface INode
    {
        string Guid { get; protected set; }
        bool Enabled { get; set; }
        ENodeState State { get; }
        ref string[] ConnectedNodesGuid { get; }
        BaseNode[] ConnectedNodes { get; }
#if UNITY_EDITOR
        string TypeName { get; set; }
        Vector2 NodePosition { get; set; }
        string Name { get; }
        string Note { get; set; }
#endif
        public static INode CreateInstance(Type nodeType)
        {
            var node = (INode)ScriptableObject.CreateInstance(nodeType);
            node.SetNewGuid();
            return node;
        }

        internal void SetNewGuid()
        {
            Guid = System.Guid.NewGuid().ToString();
        } 
        void InitNode(NodeGraph nodeGraph);
        Task Execute();
        internal Task OnEnter();
        internal Task OnRunning();
        internal Task OnExit();
        Task ChangeState(ENodeState state);
        void ResetState();

        public void AddConnection(string nodeGuid)
        {
            Array.Resize(ref ConnectedNodesGuid, ConnectedNodesGuid.Length + 1);
            ConnectedNodesGuid[^1] = nodeGuid;
        }

        public void RemoveConnection(string nodeGuid)
        {
            int index = -1;
            for (int i = 0, count = ConnectedNodesGuid.Length; i < count; i++)
            {
                if (ConnectedNodesGuid[i] == nodeGuid)
                    index = i;
                if (index > -1 && i > index)
                {
                    ConnectedNodesGuid[i-1]= ConnectedNodesGuid[i];
                }
            }
            Array.Resize(ref ConnectedNodesGuid, ConnectedNodesGuid.Length - 1);
        }
    }
}