using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class DecoratorNode : INode
    {
        public BlackBoard BlackBoard { get; } = new BlackBoard();
        public virtual string Name => this.GetType().Name;

        public ActionCallback ConditionCallback;
        public INode Action;
        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public bool Execute(IMachine machine, IState state = null)
        {
            OnExecute(machine, state);

            var result = ConditionCallback?.Invoke(machine, state) ?? false;
            if (result) return Action.Execute(machine, state);
            return false;
        }

        public void OnRegister(IMachine machine, IState state = null)
        {
            OnRegisterEvent?.Invoke(machine, state);
        }

        public void OnExit(IMachine machine, IState state)
        {
            OnExitEvent?.Invoke(machine, state);
        }

        public void OnEnter(IMachine machine, IState state)
        {
            OnEnterEvent?.Invoke(machine, state);
        }

        public void OnExecute(IMachine machine, IState state = null)
        {
            OnExecuteEvent?.Invoke(machine, state);
        }
    }
}