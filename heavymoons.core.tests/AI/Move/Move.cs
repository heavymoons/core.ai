using System;
using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Move
{
    public class Move : State
    {
        public Move()
        {
            CanChangeCallback += (machine) =>
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
            };

            OnNextEvent += (machine, state) =>
            {
                var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
                status.X += status.Dx;
                Console.WriteLine($"Move Next X: {status.X} Dx: {status.Dx}");
            };

            OnChangeEvent += (machine, state) =>
            {
                Console.WriteLine("Moving!");
            };
        }


    }
}