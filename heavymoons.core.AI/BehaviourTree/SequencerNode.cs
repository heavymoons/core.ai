using System.Collections.Generic;

namespace heavymoons.core.AI.BehaviourTree
{
    public class SequencerNode : BaseNode
    {
        public List<INode> Nodes { get; } = new List<INode>();

        public INode this[int index] => Nodes[index];

        public int Count => Nodes.Count;

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            OnExecute(machine, parentNode);
            var result = true;
            foreach (var node in Nodes)
            {
                result = node.Execute(machine, parentNode);
                if (!result) break;
            }
            ;
            return result;
        }
    }
}