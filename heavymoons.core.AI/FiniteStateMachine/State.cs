using heavymoons.core.AI.BehaviourTree;
using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.FiniteStateMachine
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public class State
    {
        public DataStorage DataStorage { get; } = new DataStorage();

        public bool Execute(StateMachine machine)
        {
            OnExecute(machine);
            if (StateMachine != null)
            {
                StateMachine.Execute(machine);
            }
            if (BehaviourMachine != null)
            {
                return BehaviourMachine.Execute(machine);
            }
            return true;
        }

        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public BehaviourMachine BehaviourMachine { get; set;}
        public StateMachine StateMachine { get; set; }

        public void OnRegister(StateMachine machine)
        {
            OnRegisterEvent?.Invoke(machine, this);
        }

        public void OnExit(StateMachine machine)
        {
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