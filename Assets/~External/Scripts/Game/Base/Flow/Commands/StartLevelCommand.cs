namespace Chillplay.OverHit.Base.Flow
{
    using LevelLoading;
    using Zenject;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    public class StartLevelCommand : LockableCommand
    {
        [Inject] 
        public IProvider<ILevelInstanceParent> LevelInstanceParent { get; set; }
        
        public override void Execute()
        {
            
        }
    }
}