namespace Chillplay.OverHit.Base.LevelLoading
{
    using OverHit.Level;

    public class Level
    {
        public int LevelNumber { get; }
        
        public LevelConfig LevelConfig { get; }
        
        public LevelOrder Order { get; }

        public Level(int levelNumber, LevelConfig levelConfig, LevelOrder order)
        {
            LevelNumber = levelNumber;
            LevelConfig = levelConfig;
            Order = order;
        }
    }
}