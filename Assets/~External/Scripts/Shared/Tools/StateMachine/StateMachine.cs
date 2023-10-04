namespace Chillplay.Tools.StateMachine
{
    using System;
    using System.Collections.Generic;
    using NaughtyAttributes;
    using Rules;
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        private State.State initialState;
        
        [SerializeField]
        public Transition[] transitions;

        public event Action<State.State> OnStateChanged;
        
        private State.State state;

        private void OnValidate()
        {
            FindReferences();
            FindTransitions();
        }

        public void Initialize()
        {
            Enter(initialState);
        }

        private void Enter(State.State state)
        {
            this.state = state;
            state.gameObject.SetActive(true);
            state.Enter();
        }

        public void ReEnter()
        {
            Enter(state);
        }

        private void ExitCurrentState()
        {
            state.Exit();
            state.gameObject.SetActive(false);
        }

        public void Stop()
        {
            if (state)
            {
                ExitCurrentState();
            }
            
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (state && HasAnyTransitions(out var nextState) ||
                state.HasTransitions(out nextState, new List<State.State>()))
            {
                ExitCurrentState();
                Enter(nextState);
                OnStateChanged?.Invoke(state);
            }
        }

        private bool HasAnyTransitions(out State.State state)
        {
            foreach (var transition in transitions)
            {
                if (transition.IsValid() && this.state != transition.To)
                {
                    state = transition.To;
                    return true;
                }
            }

            state = null;
            return false;
        }

        private void FindReferences()
        {
            var states = GetComponentsInChildren<State.State>();

            foreach (var state in states)
            {
                state.FindReferences();
            }

            var rules = GetComponentsInChildren<Rule>();
            foreach (var rule in rules)
            {
                rule.FindReferences();
            }
        }

        private void FindTransitions()
        {
            transitions = GetComponents<Transition>();
        }
    }
}