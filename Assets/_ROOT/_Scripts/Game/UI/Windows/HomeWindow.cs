namespace Chillplay.OverHit.UI
{
    using Base.Flow;
    using Chillplay.UI;
    using Shop.Flow;
    using Zenject;

    public class HomeWindow : Window
    {
        [Inject] 
        public SignalBus SignalBus { get; set; }

        protected override void BeforeClosing()
        {
            base.BeforeClosing();
            SignalBus.Fire<LevelStarted>();
        }

        public void SwitchToSettings()
        {
            SignalBus.Fire<SwitchToSettings>();
        }

        public void SwitchToShop()
        {
            SignalBus.Fire<SwitchToShop>();
        }
    }
}