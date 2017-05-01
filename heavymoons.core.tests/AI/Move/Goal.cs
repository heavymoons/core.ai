using System;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Move
{
    public class Goal : State
    {
        public Goal()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
                status.Dx = status.Dx > 1 ? status.Dx - 1 : 0;
                status.X += status.Dx;
                Console.WriteLine($"Goal Next X: {status.X} Dx: {status.Dx}");
            };

            OnEnterEvent += (machine, state) => { Console.WriteLine("Goal!"); };
        }
    }
}