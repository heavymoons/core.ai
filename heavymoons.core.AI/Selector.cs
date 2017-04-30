using System.Collections.Generic;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class Selector : IState
    {
        public BlackBoard BlackBoard { get; } = new BlackBoard();
        public virtual string Name => this.GetType().Name;

        public virtual IState CanChange(IMachine machine)
        {
            return CanChangeCallback?.Invoke(machine);
        }

        public CanChangeDelegate CanChangeCallback;

        public List<IState> Actions { get; } = new List<IState>();
        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnChangeEvent;

        public bool Execute(IMachine machine, IState state = null)
        {
            OnExecute(machine, state);
            var result = false;
            foreach (var action in Actions)
            {
                result = action.Execute(machine, state);
                if (result) break;
            };
            return result;
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