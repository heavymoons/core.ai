using System.Collections.Generic;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class SelectorNode : BaseNode
    {
        public List<INode> Actions { get; } = new List<INode>();
        public override bool Execute(IMachine machine, IState state)
        {
            OnExecute(machine, state, this);
            var result = false;
            foreach (var action in Actions)
            {
                result = action.Execute(machine, state);
                if (result) break;
            }
            ;
            return result;
        }
    }
}