namespace Chillplay.OverHit.Time
{
    public interface ITimeCounterService
    {
        ITimeCounter LevelTimeCounter { get; set; }
        ITimeCounter TotalTimeCounter { get; set; }
    }
}