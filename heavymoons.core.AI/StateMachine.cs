using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class StateMachine : INode, IMachine, IState
    {
        public virtual string Name => this.GetType().Name;

        public IState State { get; private set; } = null;

        public BlackBoard BlackBoard { get; private set; } = new BlackBoard();

        private Dictionary<string, IState> _states = new Dictionary<string, IState>();

        public ReadOnlyDictionary<string, IState> States => new ReadOnlyDictionary<string, IState>(_states);

        public void RegisterState(IState state, string name = null)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (name == null) name = state.Name;
            if (_states.ContainsKey(name)) throw new ArgumentException($"name already registered: {name}");
            _states[name] = state;
            state.OnRegister(this);
            if (State == null) State = state;
        }

        public IState GetState(string name)
        {
            if (!_states.ContainsKey(name)) throw new ArgumentException($"name not registered: {name}");
            return _states[name];
        }

        public IState GetState(Type type)
        {
            var states = _states.Where(state => state.Value.GetType() == type);
            if (!states.Any()) throw new ArgumentException($"type not registered: {type.Name} {_states.Keys}");
            if (states.Count() > 1) throw new ArgumentException($"type multiple registered: {type.Name}");
            return states.First().Value;
        }

        public bool IsState(string name)
        {
            return GetState(name) == State;
        }

        public bool IsState(Type type)
        {
            return GetState(type) == State;
        }

        public void SetState(string name)
        {
            State = GetState(name);
        }

        public void SetState(Type type)
        {
            State = GetState(type);
        }

        public virtual IState CanChange(IMachine machine)
        {
            return null;
        }

        public virtual void OnRegister(IMachine machine)
        {
            // 子ノードとして登録するステートマシンのBlackBoardは空である必要がある
            if (BlackBoard.HasRegistered) throw new InvalidOperationException($"BlackBoard already used");

            // 親のBlackBoardを引き継ぐ
            BlackBoard = machine.BlackBoard;
        }

        public virtual void OnExit(IMachine machine, IState state) {}

        public virtual void OnChange(IMachine machine, IState state) {}

        public void Next(IMachine machine = null)
        {
            if (State == null) throw new InvalidOperationException("current state not set");
            var nextState = State.CanChange(this);
            if (nextState == null)
            {
                State.Next(this);
                return;
            }
            State.OnExit(this, nextState);
            nextState.OnChange(this, State);
            State = nextState;
            State.Next(this);
            OnNext(this);
        }

        public virtual void OnNext(IMachine machine) {}
    }
}