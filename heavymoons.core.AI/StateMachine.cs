using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class StateMachine : IMachine, IState
    {
        public virtual string Name => this.GetType().Name;

        public ulong Counter { get; private set; } = 0;

        public IState State { get; private set; } = null;

        public IState NextState { get; set; } = null;

        public BlackBoard BlackBoard { get; private set; } = new BlackBoard();

        private Dictionary<string, IState> _states = new Dictionary<string, IState>();

        public ReadOnlyDictionary<string, IState> States => new ReadOnlyDictionary<string, IState>(_states);

        public void RegisterState(IState state, string name = null)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (name == null) name = state.Name;
            if (_states.ContainsKey(name)) throw new ArgumentException($"name already registered: {name}");
            _states[name] = state;
            state.OnRegister(this, State);
            if (State == null) State = state;
        }

        public void UnregisterState(string name)
        {
            var state = GetState(name);
            _states.Remove(name);
        }

        public void UnregisterState(Type type)
        {
            var states = _states.Where(state => state.Value.GetType() == type);
            if (!states.Any()) throw new ArgumentException($"type not registered: {type.Name} {_states.Keys}");
            if (states.Count() > 1) throw new ArgumentException($"type multiple registered: {type.Name}");
            _states.Remove(states.First().Key);
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

        public bool Execute(IMachine machine = null, IState state = null)
        {
            if (State == null) return false;

            Counter++;
            OnExecute(this, this);

            var result = State.Execute(this, State);

            if (State.NextState != null)
            {
                var nextState = State.NextState;
                State.OnExit(this, nextState);
                nextState.OnEnter(this, nextState);
                State = nextState;
            }
            return result;
        }

        public StateEvent OnRegisterEvent;
        public StateEvent OnExecuteEvent;
        public StateEvent OnExitEvent;
        public StateEvent OnEnterEvent;

        public void OnRegister(IMachine machine, IState state = null)
        {
            // 子ノードとして登録するステートマシンのBlackBoardは空である必要がある
            if (BlackBoard.HasRegistered) throw new InvalidOperationException($"BlackBoard already used");

            // 親のBlackBoardを引き継ぐ
            BlackBoard = machine.BlackBoard;

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