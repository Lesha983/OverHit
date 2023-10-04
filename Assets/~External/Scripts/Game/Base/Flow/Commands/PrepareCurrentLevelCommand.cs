namespace Chillplay.OverHit.Base.Flow
{
    using LevelLoading;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class PrepareCurrentLevelCommand : LockableCommand
    {
        [Inject] 
        public ILevelProvider LevelProvider { get; set; }
        
        [Inject] 
        public SignalBus SignalBus { get; set; }
        
        public override void Execute()
        {
            Lock();
            WaitLoading();
            LevelProvider.PrepareLevel();
            SignalBus.Fire(new PrepareLevel() {Level = LevelProvider.CurrentLevel});
        }

        private void WaitLoading()
        {
            SignalBus.Subscribe<LevelReadyToStart>(OnLevelReadyToStart);
            
            void OnLevelReadyToStart()
            {
                SignalBus.Unsubscribe<LevelReadyToStart>(OnLevelReadyToStart);
                Unlock();
            }
        }
    }
}