﻿using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchMachine : StateMachine
    {
        public const string Switch = "switch";

        public SwitchMachine()
        {
            DataStorage[Switch] = false;
            this["SwitchOff"] = new SwitchOff();
            this["SwitchOn"] = new SwitchOn();

            OnExecuteEvent += (machine) => { Debug.WriteLine($"Counter: {machine.Counter}"); };
        }
    }
}