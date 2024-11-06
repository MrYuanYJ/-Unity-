using UnityEditor.Experimental.GraphView;

namespace EasyFramework.Editor
{
    [Bind(typeof(SequenceNode))]
    public class SequenceNodeView : BaseNodeView
    {
        public SequenceNodeView()
        {
            //Sequence有一个输出端口一个输入端口,输入接口只能单连接，输出端口可以多连接
            Port input = CreatePort(Direction.Input, Port.Capacity.Single);
            Port output = CreatePort(Direction.Output, Port.Capacity.Multi);
            input.portName = "Input";
            output.portName = "Output";

            title = Node != null ? Node.Name : "SequenceNode";

            inputContainer.Add(input);
            outputContainer.Add(output);
        }

        public override INode Node { get; set; }
    }
}