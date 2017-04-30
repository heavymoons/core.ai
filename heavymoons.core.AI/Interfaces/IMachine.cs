using System;

namespace heavymoons.core.AI.Interfaces
{
    public interface IMachine
    {
        BlackBoard BlackBoard { get; }
        IState GetState(string name);
        IState GetState(Type type);
    }
}