using System;

namespace heavymoons.core.AI.Interfaces
{
    public interface IMachine
    {
        ulong Counter { get; }
        BlackBoard BlackBoard { get; }
        IState GetState(string name);
        IState GetState(Type type);
        IState NextState { get; set; }
    }
}