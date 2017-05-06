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
            RegisterState("Stop", new Stop());
            RegisterState("Move", new Move());
            RegisterState("Goal", new Goal());

            OnExecuteEvent += (machine) => { Debug.WriteLine($"Counter: {machine.Counter}"); };
        }
    }
}