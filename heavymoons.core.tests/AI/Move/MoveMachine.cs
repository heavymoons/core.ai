using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Move
{
    public class MoveMachine : StateMachine
    {
        public const string Status = "status";

        public MoveMachine()
        {
            DataStorage[Status] = new MoveStatus();
            this["Stop"] = new Stop();
            this["Move"] = new Move();
            this["Goal"] = new Goal();

            OnExecuteEvent += (machine) => { Debug.WriteLine($"Counter: {machine.Counter}"); };
        }
    }
}