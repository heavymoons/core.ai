using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOff : State
    {
        public SwitchOff()
        {
            OnExecuteEvent += (machine, state) =>
            {
                if (machine.DataStorage.GetValue<bool>(SwitchMachine.Switch))
                {
                    machine.NextStateName = "SwitchOn";
                }
            };
        }
    }
}