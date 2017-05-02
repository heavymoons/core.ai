using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public abstract class BaseNode : INode
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public NodeEvent OnRegisterEvent;
        public NodeEvent OnExecuteEvent;
        public NodeEvent OnExitEvent;
        public NodeEvent OnEnterEvent;

        public abstract bool Execute(IMachine machine, IState state);

        public void OnRegister(IMachine machine, IState state, INode node)
        {
            OnRegisterEvent?.Invoke(machine, state, node);
        }

        public void OnExit(IMachine machine, IState state, INode node)
        {
            OnExitEvent?.Invoke(machine, state, node);
        }

        public void OnEnter(IMachine machine, IState state, INode node)
        {
            OnEnterEvent?.Invoke(machine, state, node);
        }

        public void OnExecute(IMachine machine, IState state, INode node)
        {
            OnExecuteEvent?.Invoke(machine, state, node);
        }

    }
}