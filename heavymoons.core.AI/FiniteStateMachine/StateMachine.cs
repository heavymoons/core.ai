using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace heavymoons.core.AI.FiniteStateMachine
{
    public class StateMachine
    {
        public ulong Counter { get; protected set; }
        public DataStore DataStore { get; } = new DataStore();

        private readonly Dictionary<string, IState> _states = new Dictionary<string, IState>();

        public ReadOnlyDictionary<string, IState> States => new ReadOnlyDictionary<string, IState>(_states);

        public string CurrentState { get; private set; } = null;

        public string NextState { get; set; } = null;

        public StateMachineEvent OnExecuteEvent;

        public void OnExecute(StateMachine machine)
        {
            OnExecuteEvent?.Invoke(this);
        }

        public void RegisterState(string name, IState state)
        {
            if (_states.ContainsKey(name)) throw new ArgumentException($"name already registered: {name}");
            _states[name] = state ?? throw new ArgumentNullException(nameof(state));
            state.OnRegister(this);
            if (CurrentState == null) CurrentState = name;
        }

        public void UnregisterState(string name)
        {
            _states.Remove(name);
        }

        public IState GetState(string name)
        {
            if (!_states.ContainsKey(name)) throw new ArgumentException($"name not registered: {name}");
            return _states[name];
        }

        public bool IsCurrentState(string name)
        {
            return CurrentState == name;
        }

        /// <summary>
        /// ステート変更イベントなどを起こさずにステートを変更する
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ForceChangeState(string name)
        {
            if (!_states.ContainsKey(name)) throw new ArgumentException($"name not registered: {name}");
            CurrentState = name;
        }

        public bool Execute()
        {
            Counter++;
            OnExecute(this);

            if (CurrentState == null) return false;

            var currentState = GetState(CurrentState);
            var result = currentState.Execute(this);

            if (NextState != null)
            {
                var nextState = GetState(NextState);
                currentState.OnExit(this);
                CurrentState = NextState;
                NextState = null;
                nextState.OnEnter(this);
            }
            return result;
        }
    }
}