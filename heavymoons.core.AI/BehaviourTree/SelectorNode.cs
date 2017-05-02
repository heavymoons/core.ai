using System.Collections.Generic;

namespace heavymoons.core.AI.BehaviourTree
{
    public class SelectorNode : BaseNode
    {
        public List<INode> Nodes { get; } = new List<INode>();

        public override bool Execute(BehaviourMachine machine)
        {
            OnExecute(machine);
            var result = false;
            foreach (var node in Nodes)
            {
                result = node.Execute(machine);
                if (result) break;
            }

            return result;
        }
    }
}