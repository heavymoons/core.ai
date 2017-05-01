using System.Collections.Generic;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class SequencerNode : INode
    {
        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public virtual string Name => GetType().Name;

        public List<INode> Actions { get; } = new List<INode>();
        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnChangeEvent;

        public bool Execute(IMachine machine, IState state = null)
        {
            OnExecute(machine, state);
            var result = true;
            foreach (var action in Actions)
            {
                result = action.Execute(machine, state);
                if (!result) break;
            }
            ;
            return result;
        }

        public void OnRegister(IMachine machine, IState state = null)
        {
            OnRegisterEvent?.Invoke(machine, state);
        }

        public void OnExit(IMachine machine, IState state)
        {
            OnExitEvent?.Invoke(machine, state);
        }

        public void OnEnter(IMachine machine, IState state)
        {
            OnChangeEvent?.Invoke(machine, state);
        }

        public void OnExecute(IMachine machine, IState state = null)
        {
            OnExecuteEvent?.Invoke(machine, state);
        }
    }
}