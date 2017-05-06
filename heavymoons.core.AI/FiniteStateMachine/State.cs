using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.FiniteStateMachine
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public class State : IState
    {
        public DataStorage DataStorage { get; } = new DataStorage();

        public virtual bool Execute(StateMachine machine)
        {
            OnExecute(machine);
            return true;
        }

        public IState NextState { get; set; } = null;

        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public void OnRegister(StateMachine machine)
        {
            OnRegisterEvent?.Invoke(machine, this);
        }

        public void OnExit(StateMachine machine)
        {
            NextState = null;
            OnExitEvent?.Invoke(machine, this);
        }

        public void OnEnter(StateMachine machine)
        {
            OnEnterEvent?.Invoke(machine, this);
        }

        public void OnExecute(StateMachine machine)
        {
            OnExecuteEvent?.Invoke(machine, this);
        }
    }
}