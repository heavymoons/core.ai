using System;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Move
{
    public class MoveMachine : StateMachine
    {
        public const string Status = "status";

        public MoveMachine()
        {
            BlackBoard.Register(Status, new MoveStatus());
            RegisterState(new Stop());
            RegisterState(new Move());
            RegisterState(new Goal());

            OnExecuteEvent += (machine, status) => { Console.WriteLine($"Counter: {machine.Counter}"); };
        }
    }
}