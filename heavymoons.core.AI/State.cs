using System.Collections.Generic;
using System.Runtime.CompilerServices;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    /// <summary>
    /// ステートマシンにおけるステートのベースクラス
    /// </summary>
    public abstract class State : INode, IState
    {
        public virtual string Name => this.GetType().Name;

        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public virtual void Next(IMachine machine) {}

        public virtual IState CanChange(IMachine machine)
        {
            return null;
        }

        public virtual void OnRegister(IMachine machine) {}

        public virtual void OnExit(IMachine machine, IState state) {}

        public virtual void OnChange(IMachine machine, IState state) {}
    }
}