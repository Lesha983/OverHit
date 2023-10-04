namespace Chillplay.OverHit.Base.LevelLoading
{
    public class LevelOrder
    {
        public bool IsTutorial => Type == LevelOrderType.Tutorial;

        public string LevelId { get; }

        public LevelOrderType Type { get; }

        public LevelOrder(string levelId, LevelOrderType type)
        {
            LevelId = levelId;
            Type = type;
        }
    }

    public enum LevelOrderType
    {
        Tutorial,
        Main,
        Randomized,
        Looped
    }
}