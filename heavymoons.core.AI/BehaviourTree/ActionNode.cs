namespace heavymoons.core.AI.BehaviourTree
{
    public class ActionNode : BaseNode
    {
        public NodeCallback ActionCallback;

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            base.Execute(machine, parentNode);
            return ActionCallback.Invoke(machine, this);
        }
    }
}