using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Move
{
    public class Move : State
    {
        public Move()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var status = machine.DataStorage.GetValue<MoveStatus>(MoveMachine.Status);

                status.X += status.Dx;
                Debug.WriteLine($"Move Next X: {status.X} Dx: {status.Dx}");

                if (status.IsGoal)
                {
                    machine.NextState = "Goal";
                }
                else if (status.IsStop)
                {
                    machine.NextState = "Stop";
                }
            };

            OnEnterEvent += (machine, state) => { Debug.WriteLine("Moving!"); };
        }
    }
}