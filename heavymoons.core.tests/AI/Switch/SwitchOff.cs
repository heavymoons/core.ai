using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Switch
{
    public class SwitchOff : State
    {
        public SwitchOff()
        {
            OnExecuteEvent += (machine, state) =>
            {
                if (machine.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    state.NextState = machine.GetState(typeof(SwitchOn));
                }
            };
        }
    }
}