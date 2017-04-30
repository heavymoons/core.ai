using System;
using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Move
{
    public class Goal : State
    {
        public override void OnNext(IMachine machine)
        {
            var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
            status.Dx = status.Dx > 1 ? status.Dx - 1 : 0;
            status.X += status.Dx;
            Console.WriteLine($"Goal Next X: {status.X} Dx: {status.Dx}");
        }

        public override void OnChange(IMachine machine, IState state)
        {
            Console.WriteLine("Goal!");
        }
    }
}