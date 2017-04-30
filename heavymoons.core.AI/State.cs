using System.Collections.Generic;
using System.Runtime.CompilerServices;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public class State : INode, IState
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public virtual void Next(IMachine machine)
        {
            OnNext(machine);
        }

        public virtual IState CanChange(IMachine machine)
        {
            return CanChangeCallback?.Invoke(machine);
        }

        public CanChangeDelegate CanChangeCallback;
        public StateEvent OnRegisterEvent;
        public StateEvent OnNextEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnChangeEvent;

        public void OnRegister(IMachine machine, IState state = null)
        {
            OnRegisterEvent?.Invoke(machine, state);
        }

        public void OnExit(IMachine machine, IState state)
        {
            OnExitEvent?.Invoke(machine, state);
        }

        public void OnChange(IMachine machine, IState state)
        {
            OnChangeEvent?.Invoke(machine, state);
        }

        public void OnNext(IMachine machine, IState state = null)
        {
            OnNextEvent?.Invoke(machine, state);
        }
    }
}