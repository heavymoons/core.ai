using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.AI.BehaviourTree
{
    public class StateMachineNode : BaseNode
    {
        public StateMachine StateMachine { get; } = new StateMachine();

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            base.Execute(machine, parentNode);
            StateMachine.Execute(machine.ParentStateMachine);
            return StateMachine.NodeResult;
        }
    }
}