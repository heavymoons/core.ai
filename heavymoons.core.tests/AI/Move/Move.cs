using System;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Move
{
    public class Move : State
    {
        public Move()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);

                status.X += status.Dx;
                Console.WriteLine($"Move Next X: {status.X} Dx: {status.Dx}");

                if (status.IsGoal)
                {
                    state.NextState = machine.GetState(typeof(Goal));
                }
                else if (status.IsStop)
                {
                    state.NextState = machine.GetState(typeof(Stop));
                }
            };

            OnEnterEvent += (machine, state) => { Console.WriteLine("Moving!"); };
        }
    }
}