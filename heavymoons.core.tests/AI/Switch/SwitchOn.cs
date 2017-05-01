using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOn : State
    {
        public SwitchOn()
        {
            OnExecuteEvent += (machine, state) =>
            {
                if (!machine.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    state.NextState = machine.GetState(typeof(SwitchOff));
                }
            };
        }
    }
}