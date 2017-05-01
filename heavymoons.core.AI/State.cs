using System.Collections.Generic;
using System.Runtime.CompilerServices;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public class State : IState
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public virtual bool Execute(IMachine machine, IState state)
        {
            OnExecute(machine, state);
            return true;
        }

        public IState NextState { get; set; } = null;

        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public void OnRegister(IMachine machine, IState state = null)
        {
            OnRegisterEvent?.Invoke(machine, state);
        }

        public void OnExit(IMachine machine, IState state)
        {
            NextState = null;
            OnExitEvent?.Invoke(machine, state);
        }

        public void OnEnter(IMachine machine, IState state)
        {
            OnEnterEvent?.Invoke(machine, state);
        }

        public void OnExecute(IMachine machine, IState state = null)
        {
            OnExecuteEvent?.Invoke(machine, state);
        }
    }
}