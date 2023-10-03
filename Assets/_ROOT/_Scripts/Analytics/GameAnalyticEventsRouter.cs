namespace Chillplay.OverHit.Analytics
{
    using Chillplay.Analytics.EventRouters;
    using Base.LevelLoading;
    using Time;
    using Zenject;

    public class GameAnalyticEventsRouter : AnalyticEventsRouter
    {
        [Inject]
        public ILevelProvider LevelProvider { get; set; }
        
        [Inject] 
        public ITimeCounterService TimeCounterService { get; set; }
        
        [Inject]
        public SignalBus SignalBus { get; set; }
        
        protected override void Subscribe()
        {
           
        }
        
        protected override void Unsubscribe()
        {
           
        }
    }
}