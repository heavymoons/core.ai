namespace heavymoons.core.AI.FiniteStateMachine
{
    public delegate void StateMachineEvent(StateMachine machine);

    public delegate void StateEvent(StateMachine machine, IState state);

    public delegate bool StateCallback(StateMachine machine, IState state);
}