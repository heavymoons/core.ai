using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.FiniteStateMachine
{
    public class StateMachine
    {
        public ulong Counter { get; protected set; }
        public DataStorage DataStorage { get; } = new DataStorage();

        private readonly Dictionary<string, State> _states = new Dictionary<string, State>();

        public string CurrentState { get; private set; } = null;

        public string NextState { get; set; } = null;

        public StateMachineEvent OnExecuteEvent;

        private StateMachine _parentStateMachine = null;
        public StateMachine ParentStateMachine => _parentStateMachine;

        public void OnExecute(StateMachine machine)
        {
            OnExecuteEvent?.Invoke(this);
        }

        public void RegisterState(string name, State state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (_states.ContainsKey(name)) throw new ArgumentException($"name already registered: {name}");
            _states[name] = state;
            state.OnRegister(this);
            if (CurrentState == null) CurrentState = name;
        }

        public void UnregisterState(string name)
        {
            _states.Remove(name);
        }

        public State GetState(string name)
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

        public bool Execute(StateMachine parentMachine = null)
        {
            _parentStateMachine = parentMachine;

            Debug.WriteLine($"StateMachine Execute");
            Counter++;
            Debug.WriteLine($"Counter: {Counter}");
            OnExecute(this);

            if (CurrentState == null) return false;

            Debug.WriteLine($"CurrentState: {CurrentState}");
            var currentState = GetState(CurrentState);
            var result = currentState.Execute(this);

            if (NextState != null)
            {
                Debug.WriteLine($"NextState: {NextState}");
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