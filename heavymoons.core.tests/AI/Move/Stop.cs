using System;
using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Move
{
    public class Stop : State
    {
        public Stop()
        {
            CanChangeCallback += (machine) =>
            {
                var playerStatus = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);

                if (!playerStatus.IsStop)
                {
                    return machine.GetState(typeof(Move));
                }
                return null;
            };

            OnChangeEvent += (machine, state) =>
            {
                Console.WriteLine("Stopped!");
            };
        }
    }
}