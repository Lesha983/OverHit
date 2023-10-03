namespace Chillplay.Tools.StateMachine.State
{
    using System.Collections.Generic;
    using NaughtyAttributes;
    using UnityEngine;

    public abstract class State : MonoBehaviour, IState
    {
        public bool CanBeExited => canBeExited;

        [SerializeField]
        private bool canBeExited = true;
        [SerializeField, ReadOnly]
        private Transition[] transitions;

        protected virtual void Awake()
        {
            gameObject.SetActive(false);
        }
        
        private void OnValidate()
        {
            FindReferences();
        }

        public virtual void FindReferences()
        {
            transitions = GetComponents<Transition>();
        }

        public abstract void Enter();

        public abstract void Exit();

        public bool HasTransitions(out State state, List<State> currentStates)
        {
            currentStates.Add(this);

            foreach (var transition in transitions)
            {
                if (!currentStates.Contains(transition.To) && transition.IsValid())
                {
                    state = transition.To;
                    if (state.canBeExited && state.HasTransitions(out var nextState, currentStates))
                    {
                        state = nextState;
                    }

                    return true;
                }
            }

            state = null;
            return false;
        }
    }
}