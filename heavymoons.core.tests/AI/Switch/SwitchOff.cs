using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOff : State
    {
        public SwitchOff()
        {
            CanChangeCallback += (machine) =>
            {
                if (machine.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    return machine.GetState(typeof(SwitchOn));
                }
                return null;
            };

        }
    }
}