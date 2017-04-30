using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public delegate void StateEvent(IMachine machine, IState state);

    public delegate IState CanChangeDelegate(IMachine machine);
}