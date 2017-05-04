using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Move
{
    public class Stop : State
    {
        public Stop()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var playerStatus = machine.DataStorage.GetValue<MoveStatus>(MoveMachine.Status);

                if (!playerStatus.IsStop)
                {
                    machine.NextState = "Move";
                }
            };

            OnEnterEvent += (machine, state) => { Debug.WriteLine("Stopped!"); };
        }
    }
}