using heavymoons.core.AI;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOn : State
    {
        public override IState CanChange(IMachine machine)
        {
            if (!machine.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
            {
                return machine.GetState(typeof(SwitchOff));
            }
            return null;
        }
    }
}