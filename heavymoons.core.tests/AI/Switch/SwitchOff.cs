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
                if (machine.DataStore.GetValue<bool>(SwitchMachine.Switch))
                {
                    machine.NextState = "SwitchOn";
                }
            };
        }
    }
}