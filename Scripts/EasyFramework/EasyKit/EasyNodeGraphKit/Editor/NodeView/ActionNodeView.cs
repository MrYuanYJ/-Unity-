using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace EasyFramework.Editor
{
    [Bind(typeof(ActionNode))]
    [Menu("Action Node/BaseActionNode")]
    public class ActionNodeView: BaseNodeView  
    {
        public ActionNodeView()
        {
            Port inputPort = CreatePort(Direction.Input, Port.Capacity.Multi);
            Port outputPort = CreatePort(Direction.Output, Port.Capacity.Single);
            inputPort.portName = "Input";
            outputPort.portName = "Output";
            
            title= Node!= null? Node.Name : "Action Node";
            inputContainer.Add(inputPort);
            outputContainer.Add(outputPort);
        }

        public override INode Node { get; set; }
    }
}