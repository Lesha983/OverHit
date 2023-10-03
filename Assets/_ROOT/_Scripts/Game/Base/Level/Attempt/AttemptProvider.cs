namespace Chillplay.OverHit.Base
{
    using System;
    using Saves;
    using Flow;
    using Zenject;

    public interface IAttemptProvider
    {
        int CurrentAttempt { get; }
        int OverallStartedLevels { get; }
    }

    public class AttemptProvider : SaveableComponent<LastAttemptSave>, IAttemptProvider
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        public int CurrentAttempt { get; private set; } = 0;

        public int OverallStartedLevels { get; private set; } = 0;
        
        public override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<SwitchingToGame>(OnLevelStarted);
            SignalBus.Subscribe<LevelCompleted>(Reset);
        }

        private void OnLevelStarted()
        {
            OverallStartedLevels++;
            CurrentAttempt++;
            ScheduleSave();
        }
        
        private void Reset()
        {
            CurrentAttempt = 0;
            ScheduleSave(); 
        }
        
        protected override LastAttemptSave PrepareInitialSave()
        {
            return new LastAttemptSave
            {
                OverallStartedLevels = OverallStartedLevels,
                Attempt = CurrentAttempt
            };
        }

        public override LastAttemptSave Serialize()
        {
            return new LastAttemptSave
            {
                OverallStartedLevels = OverallStartedLevels,
                Attempt = CurrentAttempt
            };
        }

        public override void Deserialize(LastAttemptSave save, DateTime lastSaveTime)
        {
            OverallStartedLevels = save.OverallStartedLevels;
            CurrentAttempt = save.Attempt;
        }
    }
}