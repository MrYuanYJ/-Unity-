using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    [System.Serializable]
    public class BaseNode: ScriptableObject,INode
    {
        #region Serializable
        
#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        public string typeName;
        [SerializeField, HideInInspector]
        Vector2 nodePos;
        [SerializeField] 
        protected new string name;
        [SerializeField,TextArea,Space]
        protected string note;
#endif

        [SerializeField, HideInInspector]
        protected string guid;
        [SerializeField, HideInInspector]
        protected string[] connectedNodesGuid = Array.Empty<string>();
        [NonSerialized]
        public BaseNode[] connectedNodes;

        #endregion


        string INode.Guid { get => guid; set => guid = value; }
        public bool Enabled { get; set; } = true;
        public ENodeState State { get; private set; }

        ref string[] INode.ConnectedNodesGuid => ref connectedNodesGuid;
        BaseNode[] INode.ConnectedNodes => connectedNodes;

#if UNITY_EDITOR
        public string TypeName { get=> typeName; set=> typeName = value;}
        public Vector2 NodePosition { get=> nodePos; set=> nodePos = value;}
        public virtual string Name => String.IsNullOrEmpty(name)? GetType().Name : name;
        public string Note { get=> note; set=> note = value;}
#endif

        public void InitNode(NodeGraph nodeGraph)
        {
            connectedNodes = new BaseNode[connectedNodesGuid.Length];
            for (int i = 0; i < connectedNodes.Length; i++)
            {
                connectedNodes[i] = nodeGraph.NodeDict[connectedNodesGuid[i]];
            }
        }

        public virtual async Task Execute()
        {
            if (State != ENodeState.None)
                return;
            
            await ChangeState(ENodeState.Enter);
        }

        Task INode.OnEnter() => OnEnter();
        Task INode.OnRunning() => OnRunning();
        Task INode.OnExit() => OnExit();
        
        protected virtual async Task OnEnter(){await Task.CompletedTask;}
        protected virtual async Task OnRunning() => await ChangeState(ENodeState.Exit);
        protected virtual async Task OnExit(){await Task.CompletedTask;}

        public async Task ChangeState(ENodeState state)
        {
            if (Enabled && State == state && state != ENodeState.Running)
                return;
            State = state;
            var node = (INode) this;
            switch (state)
            {
                case ENodeState.Enter:
                    await node.OnEnter();
                    await ChangeState(ENodeState.Running);
                    break;
                case ENodeState.Running:
                    await node.OnRunning();
                    break;
                case ENodeState.Exit:
                    await node.OnExit();
                    await ChangeState(ENodeState.Finished);
                    break;
                case ENodeState.Finished:
                    foreach (var next in connectedNodes)
                       await next.Execute();
                    await ChangeState(ENodeState.None);
                    break;
            }
        }

        public void ResetState()=> ChangeState(ENodeState.None).ViewError();
    }
}