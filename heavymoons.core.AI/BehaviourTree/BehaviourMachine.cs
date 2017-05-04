using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.BehaviourTree
{
    public class BehaviourMachine
    {
        public ulong Counter { get; protected set; }
        public DataStorage DataStorage { get; } = new DataStorage();

        public INode Node { get; set; }

        public BehaviourMachineEvent OnExecuteEvent;

        public void OnExecute(BehaviourMachine machine)
        {
            OnExecuteEvent?.Invoke(this);
        }

        public bool Execute()
        {
            Counter++;
            OnExecute(this);
            if (Node == null) return false;

            return Node.Execute(this);
        }
    }
}