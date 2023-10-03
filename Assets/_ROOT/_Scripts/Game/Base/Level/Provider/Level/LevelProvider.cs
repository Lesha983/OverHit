namespace Chillplay.OverHit.Base.LevelLoading
{
    using System;
    using Saves;
    using Zenject;

    public class LevelProvider : SaveableComponent<LevelSave>, ILevelProvider
    {
        [Inject] 
        public ILevelBuilder LevelBuilder { get; set; }

        public int CurrentLevelNumber => CurrentLevel?.LevelNumber ?? currentLevelNumber;
        
        public Level CurrentLevel { get; private set; }
        
        private int currentLevelNumber;

        public void MoveToNext()
        {
            MoveTo(CurrentLevelNumber + 1);
        }

        public void MoveTo(int levelNumber)
        {
            currentLevelNumber = levelNumber;
            ScheduleSave();
        }

        public void PrepareLevel()
        {
            var level = LevelBuilder.BuildLevel(currentLevelNumber);
            CurrentLevel = level;
        }
        
        protected override LevelSave PrepareInitialSave()
        {
            return new LevelSave
            {
                Level = 1
            };
        }

        public override LevelSave Serialize()
        {
            return new LevelSave()
            {
                Level = CurrentLevelNumber
            };
        }

        public override void Deserialize(LevelSave save, DateTime lastSaveTime)
        {
            currentLevelNumber = save.Level;
            CurrentLevel = LevelBuilder.BuildLevel(CurrentLevelNumber);
        }
    }
}