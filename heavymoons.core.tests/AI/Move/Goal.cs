using System;
using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Move
{
    public class Goal : State
    {
        public Goal()
        {
            OnNextEvent += (machine, state) =>
            {
                var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
                status.Dx = status.Dx > 1 ? status.Dx - 1 : 0;
                status.X += status.Dx;
                Console.WriteLine($"Goal Next X: {status.X} Dx: {status.Dx}");
            };

            OnChangeEvent += (machine, state) =>
            {
                Console.WriteLine("Goal!");
            };
        }
    }
}