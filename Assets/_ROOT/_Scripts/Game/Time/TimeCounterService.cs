namespace Chillplay.OverHit.Time
{
    using Base.Flow;
    using Zenject;

    public class TimeCounterService : IInitializable, ITimeCounterService
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        [Inject]
        public ITimeCounter LevelTimeCounter { get; set; }

        [Inject]
        public ITimeCounter TotalTimeCounter { get; set; }

        public void Initialize()
        {
            TotalTimeCounter.Initialize();
            
            SignalBus.Subscribe<LevelStarted>(OnLevelStarted);
            SignalBus.Subscribe<ILevelEnding>(OnLevelEnding);
        }

        private void OnLevelStarted()
        {
            LevelTimeCounter.Initialize();
        }

        private void OnLevelEnding()
        {
            LevelTimeCounter.Pause();
        }
    }
}