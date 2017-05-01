using System;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Move
{
    public class Stop : State
    {
        public Stop()
        {
            OnExecuteEvent += (machine, state) =>
            {
                var playerStatus = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);

                if (!playerStatus.IsStop)
                {
                    state.NextState = machine.GetState(typeof(Move));
                }
            };

            OnEnterEvent += (machine, state) => { Console.WriteLine("Stopped!"); };
        }
    }
}