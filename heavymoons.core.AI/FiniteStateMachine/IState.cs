using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.FiniteStateMachine
{
    public interface IState
    {
        DataStorage DataStorage { get; }
        string Name { get; }
        bool Execute(StateMachine machine);
        void OnExecute(StateMachine machine);
        void OnRegister(StateMachine machine);
        void OnExit(StateMachine machine);
        void OnEnter(StateMachine machine);
    }
}