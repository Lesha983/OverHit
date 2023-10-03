namespace Chillplay.OverHit.Base.Configs
{
    using Chillplay.Configs;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(LevelDelays), menuName = "Configs/LevelDelays", order = 0)]
    public class LevelDelays : Config
    {
        public float End;

        public float Fail;

        public float Complete;
    }
}