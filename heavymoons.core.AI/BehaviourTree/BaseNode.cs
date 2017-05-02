namespace heavymoons.core.AI.BehaviourTree
{
    public abstract class BaseNode : INode
    {
        public virtual string Name => this.GetType().Name;

        public DataStore DataStore { get; } = new DataStore();

        public NodeEvent OnExecuteEvent;

        public abstract bool Execute(BehaviourMachine machine);

        public void OnExecute(BehaviourMachine machine)
        {
            OnExecuteEvent?.Invoke(machine, this);
        }
    }
}