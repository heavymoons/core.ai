namespace heavymoons.core.AI.BehaviourTree
{
    public class ActionNode : BaseNode
    {
        public NodeCallback ActionCallback;

        public override bool Execute(BehaviourMachine machine)
        {
            OnExecute(machine);
            return ActionCallback.Invoke(machine, this);
        }
    }
}