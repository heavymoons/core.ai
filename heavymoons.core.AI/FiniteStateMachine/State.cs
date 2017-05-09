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

        public void Execute(StateMachine machine)
        {
            OnExecute(machine);
            StateMachine?.Execute(machine);
            if (BehaviourMachine != null)
            {
                machine.NodeResult = BehaviourMachine.Execute(machine);
            }
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