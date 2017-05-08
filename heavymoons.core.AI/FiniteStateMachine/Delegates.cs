namespace heavymoons.core.AI.FiniteStateMachine
{
    public delegate void StateMachineEvent(StateMachine machine);

    public delegate void StateEvent(StateMachine machine, State state);

    public delegate bool StateCallback(StateMachine machine, State state);
}