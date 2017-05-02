using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public abstract class BaseNode : INode
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public NodeEvent OnExecuteEvent;

        public abstract bool Execute(IMachine machine, IState state);

        public void OnExecute(IMachine machine, IState state, INode node)
        {
            OnExecuteEvent?.Invoke(machine, state, node);
        }

    }
}