using System.Collections.Generic;

namespace heavymoons.core.AI.BehaviourTree
{
    public class SequencerNode : BaseNode
    {
        public List<INode> Nodes { get; } = new List<INode>();

        public override bool Execute(BehaviourMachine machine)
        {
            OnExecute(machine);
            var result = true;
            foreach (var node in Nodes)
            {
                result = node.Execute(machine);
                if (!result) break;
            }
            ;
            return result;
        }
    }
}