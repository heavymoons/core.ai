using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public delegate void StateEvent(IMachine machine, IState state);
    public delegate void NodeEvent(IMachine machine, IState state, INode node);

    public delegate bool ActionCallback(IMachine machine, IState state, INode node);
}