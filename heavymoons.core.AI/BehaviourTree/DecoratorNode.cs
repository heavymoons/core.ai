namespace heavymoons.core.AI.BehaviourTree
{
    public class DecoratorNode : BaseNode
    {
        public NodeCallback ConditionCallback { get; set; }
        public INode Node { get; set; }

        public override bool Execute(BehaviourMachine machine)
        {
            OnExecute(machine);

            var result = ConditionCallback?.Invoke(machine, this) ?? false;
            if (result) return Node.Execute(machine);

            return false;
        }
    }
}