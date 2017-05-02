using System;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class BehaviourState : State
    {
        public INode Node { get; set; } = null;

        public BehaviourState():base() {}

        public BehaviourState(string name) : base(name)
        {
        }

        public override bool Execute(IMachine machine)
        {
            var result = Node.Execute(machine, this);
            OnExecute(machine, this);
            return true;
        }
    }
}