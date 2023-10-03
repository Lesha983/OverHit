namespace Chillplay.OverHit.Time
{
    public interface ITimeCounter
    {
        float Time { get; }
        bool Paused { get; }
        void Initialize();
        void Pause();
        void Resume();
    }
}