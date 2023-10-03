namespace Chillplay.Tools.SimpleStateMachine
{
    using NaughtyAttributes;
    using UnityEngine;

    public abstract class State : MonoBehaviour, IState
    {
        public abstract void FindReferences();

        public abstract void Enter();

        public abstract void Exit();

        protected TReference FindReference<TReference>()
            where TReference : Component
        {
            return GetComponentInParent<TReference>(true);
        }
    }

    public abstract class State<TState> : State
        where TState : State
    {
        [SerializeField, ReadOnly]
        protected SimpleStateMachine<TState> stateMachine;

        public override void FindReferences()
        {
            stateMachine = FindReference<SimpleStateMachine<TState>>();
        }

        public override void Enter()
        {
            //
        }

        public override void Exit()
        {
            //
        }
    }

    public abstract class State<TState, TMachine> : State
        where TState : State
        where TMachine : SimpleStateMachine<TState>
    {
        [SerializeField, ReadOnly]
        protected TMachine stateMachine;

        public override void FindReferences()
        {
            stateMachine = FindReference<TMachine>();
        }

        public override void Enter()
        {
            //
        }

        public override void Exit()
        {
            //
        }
    }
}