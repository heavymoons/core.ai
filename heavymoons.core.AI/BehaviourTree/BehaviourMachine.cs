using heavymoons.core.AI.BlackBoard;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.AI.BehaviourTree
{
    public class BehaviourMachine
    {
        public ulong Counter { get; protected set; }
        public DataStorage DataStorage { get; } = new DataStorage();

        private INode _rootNode = null;

        public void RegisterRootNode(INode node)
        {
            _rootNode = node;
        }

        private StateMachine _parentStateMachine = null;
        public StateMachine ParentStateMachine => _parentStateMachine;

        public BehaviourMachineEvent OnExecuteEvent;

        public void OnExecute(BehaviourMachine machine)
        {
            OnExecuteEvent?.Invoke(this);
        }

        public bool Execute(StateMachine parentStateMachine = null)
        {
            _parentStateMachine = parentStateMachine;

            Counter++;
            OnExecute(this);
            if (_rootNode == null) return false;

            return _rootNode.Execute(this, null);
        }
    }
}