namespace heavymoons.core.AI.BehaviourTree
{
    public class DecoratorNode : BaseNode
    {
        public NodeCallback ConditionCallback { get; set; }
        public INode Node { get; set; }

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            OnExecute(machine, parentNode);

            var result = ConditionCallback?.Invoke(machine, this, parentNode) ?? false;
            if (result) return Node.Execute(machine, parentNode);

            return false;
        }
    }
}