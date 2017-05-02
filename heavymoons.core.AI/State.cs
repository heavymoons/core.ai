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
        private readonly string _name = null;
        public string Name => _name ?? GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public State() {}
        public State(string name) : this()
        {
            _name = name;
        }

        public virtual bool Execute(IMachine machine)
        {
            OnExecute(machine, this);
            return true;
        }

        public IState NextState { get; set; } = null;

        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public void OnRegister(IMachine machine, IState state)
        {
            OnRegisterEvent?.Invoke(machine, this);
        }

        public void OnExit(IMachine machine, IState state)
        {
            NextState = null;
            OnExitEvent?.Invoke(machine, this);
        }

        public void OnEnter(IMachine machine, IState state)
        {
            OnEnterEvent?.Invoke(machine, this);
        }

        public void OnExecute(IMachine machine, IState state)
        {
            OnExecuteEvent?.Invoke(machine, this);
        }
    }
}