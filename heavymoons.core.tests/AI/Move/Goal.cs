using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Move
{
    public class Goal : State
    {
        public Goal()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var status = machine.DataStore.GetValue<MoveStatus>(MoveMachine.Status);
                status.Dx = status.Dx > 1 ? status.Dx - 1 : 0;
                status.X += status.Dx;
                Debug.WriteLine($"Goal Next X: {status.X} Dx: {status.Dx}");
            };

            OnEnterEvent += (machine, state) => { Debug.WriteLine("Goal!"); };
        }
    }
}