namespace Chillplay.OverHit.Base.LevelLoading
{
    public interface ILevelProvider
    {
        int CurrentLevelNumber { get; }
        Level CurrentLevel { get; }
        void MoveToNext();
        void MoveTo(int levelNumber);
        void PrepareLevel();
    }
}