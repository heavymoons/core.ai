using System.Collections.Generic;

namespace heavymoons.core.AI.BehaviourTree
{
    public class SequencerNode : BaseNode
    {
        public List<INode> ChildNodes { get; } = new List<INode>();

        public INode this[int index] => ChildNodes[index];

        public int Count => ChildNodes.Count;

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            base.Execute(machine, parentNode);

            var result = true;
            foreach (var node in ChildNodes)
            {
                result = node.Execute(machine, this);
                if (!result) break;
            }
            ;
            return result;
        }
    }
}