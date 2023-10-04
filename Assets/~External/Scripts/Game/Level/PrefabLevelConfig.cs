namespace Chillplay.OverHit.Level
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Level", menuName = "Configs/PrefabLevel", order = 0)]
    public class PrefabLevelConfig : LevelConfig
    {
        public LevelArea LevelArea;
    }
}