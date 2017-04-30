using System;
using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Move
{
    public class Move : State
    {
        public override IState CanChange(IMachine machine)
        {
            var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);

            if (status.IsGoal)
            {
                return machine.GetState(typeof(Goal));
            }
            if (status.IsStop)
            {
                return machine.GetState(typeof(Stop));
            }
            return null;
        }

        public override void OnNext(IMachine machine)
        {
            var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
            status.X += status.Dx;
            Console.WriteLine($"Move Next X: {status.X} Dx: {status.Dx}");
        }

        public override void OnChange(IMachine machine, IState state)
        {
            Console.WriteLine("Moving!");
        }
    }
}