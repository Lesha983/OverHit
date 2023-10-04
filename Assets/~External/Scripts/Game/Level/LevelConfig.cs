namespace Chillplay.OverHit.Level
{
    using UnityEngine;
    
    public abstract class LevelConfig : ScriptableObject
    {
        public bool IsTutorial { get; set; }
    }
}