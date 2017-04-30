using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOn : State
    {
        public SwitchOn()
        {
            CanChangeCallback += (machine) =>
            {
                if (!machine.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    return machine.GetState(typeof(SwitchOff));
                }
                return null;
            };
        }
    }
}