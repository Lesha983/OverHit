namespace Chillplay.OverHit.Base.Level
{
    using System;
    using Flow;
    using Zenject;

    public class LevelStateProvider : IInitializable, ILevelStateProvider
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        public event Action<LevelState, LevelState> LevelStateChanged;

        public LevelState CurrentState
        {
            get => currentState;
            private set
            {
                PreviousState = currentState;
                currentState = value;
                if (PreviousState != currentState)
                {
                    LevelStateChanged?.Invoke(PreviousState, currentState);
                }
            }
        }

        public LevelState PreviousState { get; private set; } = LevelState.None;

        private LevelState currentState = LevelState.None;
        
        
        public void Initialize()
        {
            SignalBus.Subscribe<PrepareLevel>(_ => SetStateTo(LevelState.Preparing));
            SignalBus.Subscribe<LevelReadyToStart>(_ => SetStateTo(LevelState.Prepared));
            SignalBus.Subscribe<LevelStarted>(_ => SetStateTo(LevelState.Started));
            SignalBus.Subscribe<LevelCompleting>(_ => SetStateTo(LevelState.Completing));
            SignalBus.Subscribe<LevelFailing>(_ => SetStateTo(LevelState.Failing));
            SignalBus.Subscribe<LevelCompleted>(_ => SetStateTo(LevelState.Completed));
            SignalBus.Subscribe<LevelFailed>(_ => SetStateTo(LevelState.Failed));
        }
        
        private void SetStateTo(LevelState targetState)
        {
            CurrentState = targetState;
        }
    }
}