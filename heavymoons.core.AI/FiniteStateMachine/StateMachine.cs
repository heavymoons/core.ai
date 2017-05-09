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

        public string CurrentStateName { get; private set; } = null;

        public string NextStateName { get; set; } = null;

        public StateMachineEvent OnExecuteEvent;

        /// <summary>
        /// ビヘイビアツリーのノードとしてステートマシンが動作するときのリザルト
        /// </summary>
        public bool NodeResult { get; set; }

        private StateMachine _parentStateMachine = null;
        public StateMachine ParentStateMachine => _parentStateMachine;

        public void OnExecute(StateMachine machine)
        {
            OnExecuteEvent?.Invoke(this);
        }

        public State this[string name]
        {
            get
            {
                return GetState(name);
            }
            set
            {
                RegisterState(name, value);
            }
        }

        public void RegisterState(string name, State state)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (_states.ContainsKey(name)) throw new ArgumentException($"name already registered: {name}");
            _states[name] = state;
            state.OnRegister(this);
            if (CurrentStateName == null) CurrentStateName = name;
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
            return CurrentStateName == name;
        }

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="withoutEvent"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ChangeState(string name, bool withoutEvent = true)
        {
            if (!_states.ContainsKey(name)) throw new ArgumentException($"name not registered: {name}");
            if (withoutEvent)
            {
                CurrentStateName = name;
            }
            else
            {
                if (!string.IsNullOrEmpty(CurrentStateName))
                {
                    var currentState = GetState(CurrentStateName);
                    currentState.OnExit(this);
                }
                CurrentStateName = name;
                var nextState = GetState(CurrentStateName);
                nextState.OnEnter(this);
            }
        }

        public bool Execute(StateMachine parentMachine = null)
        {
            _parentStateMachine = parentMachine;
            NodeResult = false;

            Debug.WriteLine($"StateMachine Execute");
            Counter++;
            Debug.WriteLine($"Counter: {Counter}");
            OnExecute(this);

            if (CurrentStateName == null) return NodeResult;

            Debug.WriteLine($"CurrentState: {CurrentStateName}");
            var currentState = GetState(CurrentStateName);
            currentState.Execute(this);

            if (!string.IsNullOrEmpty(NextStateName))
            {
                Debug.WriteLine($"NextState: {NextStateName}");
                var nextState = GetState(NextStateName);
                currentState.OnExit(this);
                CurrentStateName = NextStateName;
                NextStateName = null;
                nextState.OnEnter(this);
            }
            return NodeResult;
        }
    }
}