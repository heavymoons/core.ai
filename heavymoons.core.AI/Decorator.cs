using System.Dynamic;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class Decorator: IState
    {
        public BlackBoard BlackBoard { get; } = new BlackBoard();
        public virtual string Name => this.GetType().Name;

        public virtual IState CanChange(IMachine machine)
        {
            return CanChangeCallback?.Invoke(machine);
        }

        public CanChangeDelegate CanChangeCallback;

        public ActionCallback ConditionCallback;
        public IState Action;
        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnChangeEvent;

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

        public void OnChange(IMachine machine, IState state)
        {
            OnChangeEvent?.Invoke(machine, state);
        }

        public void OnExecute(IMachine machine, IState state = null)
        {
            OnExecuteEvent?.Invoke(machine, state);
        }
    }
}