using System;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class BehaviorState : State
    {
        public INode Node { get; set; } = null;

        public override bool Execute(IMachine machine, IState state = null)
        {
            var result = Node.Execute(machine, this);
            OnExecute(machine, state);
            return true;
        }
    }
}