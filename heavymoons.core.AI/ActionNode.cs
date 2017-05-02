using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class ActionNode : BaseNode
    {
        public ActionCallback ActionCallback;

        public override bool Execute(IMachine machine, IState state)
        {
            OnExecute(machine, state, this);
            return ActionCallback.Invoke(machine, state, this);
        }
    }
}