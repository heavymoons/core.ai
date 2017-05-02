using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOn : State
    {
        public SwitchOn()
        {
            OnExecuteEvent += (machine, state) =>
            {
                if (!machine.DataStore.GetValue<bool>(SwitchMachine.Switch))
                {
                    machine.NextState = "SwitchOff";
                }
            };
        }
    }
}