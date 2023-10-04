namespace Chillplay.OverHit.Time
{
    using Zenject;

    public class TimeCounter : ITickable, ITimeCounter
    {
        public float Time { get; private set; }

        public bool Paused { get; private set; }

        public TimeCounter(TickableManager tickableManager)
        {
            tickableManager.Add(this);
        }

        public void Initialize()
        {
            Time = 0;
            Paused = false;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }
        
        public void Tick()
        {
            Time += Paused ? 0 : UnityEngine.Time.unscaledDeltaTime;
        }
    }
}