namespace Chillplay.OverHit.Base.Configs
{
    using System.Collections.Generic;
    using System.Linq;
    using Chillplay.Configs;
    using LevelLoading;
    using OverHit.Level;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(LevelsOrder), menuName = "Configs/LevelsOrder", order = 0)]
    public class LevelsOrder : Config
    {
        public bool CyclingEnabled;
        public bool RandomEnabled;

        public List<LevelConfig> TutorialLevels;
        public List<LevelConfig> MainLevels;

        public (LevelConfig, LevelOrder) GetLevelConfig(int levelNumber)
        {
            var levelIndex = levelNumber - 1;

            if (levelIndex < TutorialLevels.Count)
            {
                var config = TutorialLevels[levelIndex];
                var order = new LevelOrder(config.name, LevelOrderType.Tutorial);

                return (config, order);
            }

            levelIndex -= TutorialLevels.Count;

            if (levelIndex < MainLevels.Count)
            {
                var config = MainLevels[levelIndex];
                var order = new LevelOrder(config.name, LevelOrderType.Main);
                
                return (config, order);
            }

            if (RandomEnabled)
            {
                int randomIndex = Random.Range(0, MainLevels.Count);
                
                var config = MainLevels[randomIndex];
                var order = new LevelOrder(config.name, LevelOrderType.Randomized);

                return (config, order);
            }

            if (CyclingEnabled)
            {
                var cycle = (levelIndex - MainLevels.Count) % MainLevels.Count;
                
                var config = MainLevels[cycle];
                var order = new LevelOrder(config.name, LevelOrderType.Looped);

                return (config, order);
            }

            var lastConfig = MainLevels.Last();
            var lastOrder = new LevelOrder(lastConfig.name, LevelOrderType.Main);

            return (lastConfig, lastOrder);
        }
    }
}