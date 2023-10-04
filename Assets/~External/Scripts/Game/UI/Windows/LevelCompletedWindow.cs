namespace Chillplay.OverHit.UI
{
    using Base.Flow;
    using Chillplay.UI;
    using Zenject;

    public class LevelCompletedWindow : Window
    {
        [Inject] 
        public SignalBus SignalBus { get; set; }

        private bool closing;

        public void StartNext()
        {
            if(closing) return;
            closing = true;
            SignalBus.AbstractFire<LevelCompleted>();
            Close();
        }
    }
}