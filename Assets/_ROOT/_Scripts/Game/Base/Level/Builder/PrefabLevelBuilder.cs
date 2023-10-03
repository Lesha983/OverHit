namespace Chillplay.OverHit.Base.LevelLoading
{
    using Configs;
    using Zenject;

    public class PrefabLevelBuilder : ILevelBuilder
    {
        [Inject]
        public LevelsOrder LevelsOrder { get; set; }
        
        public Level BuildLevel(int levelNumber)
        {
            var levelTuple = LevelsOrder.GetLevelConfig(levelNumber);

            return new Level(levelNumber, levelTuple.Item1, levelTuple.Item2);
        }
    }
}