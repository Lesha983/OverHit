namespace Chillplay.OverHit.UI
{
    using Base.Flow;
    using Chillplay.UI;
    using Zenject;

    public class LevelFailedWindow : Window
    {
        [Inject] 
        public SignalBus SignalBus { get; set; }

        private bool closing;

        public void Restart()
        {
            if(closing) return;
            closing = true;
            SignalBus.AbstractFire<LevelFailed>();
            Close();
        }
    }
}