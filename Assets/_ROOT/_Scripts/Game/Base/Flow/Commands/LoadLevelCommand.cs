namespace Chillplay.OverHit.Base.Flow
{
    using LevelLoading;
    using Zenject;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    public class LoadLevelCommand : LockableCommand
    {
        [Inject] 
        public PrepareLevel PrepareLevel { get; set; }
        
        [Inject] 
        public IProvider<ILevelInstanceParent> LevelInstanceParent { get; set; }
        
        [Inject] 
        public SignalBus SignalBus { get; set; }

        private Level Level => PrepareLevel.Level;
        
        public override async void Execute()
        {
            Lock();
            await LevelInstanceParent.Instance.SetLevel(Level.LevelConfig);
            SignalBus.Fire(new LevelReadyToStart() {Level = Level});
            Unlock();
        }
    }
}