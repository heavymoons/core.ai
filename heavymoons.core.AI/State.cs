using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public class State : IState
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public virtual bool Execute(IMachine machine, IState state = null)
        {
            OnExecute(machine, state);
            return true;
        }

        public virtual IState CanChange(IMachine machine)
        {
            return CanChangeCallback?.Invoke(machine);
        }

        public CanChangeDelegate CanChangeCallback;
        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnChangeEvent;

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