using System;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchMachine : StateMachine
    {
        public const string Switch = "switch";

        public SwitchMachine()
        {
            BlackBoard.Register(Switch, false);
            RegisterState(new SwitchOff());
            RegisterState(new SwitchOn());

            OnExecuteEvent += (machine, status) => { Console.WriteLine($"Counter: {machine.Counter}"); };
        }
    }
}