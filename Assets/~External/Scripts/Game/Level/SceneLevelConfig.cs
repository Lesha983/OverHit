namespace Chillplay.OverHit.Level
{
    using NaughtyAttributes;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Level", menuName = "Configs/SceneLevel", order = 0)]
    public class SceneLevelConfig : LevelConfig
    {
        [Scene]
        public string LevelId;
    }
}