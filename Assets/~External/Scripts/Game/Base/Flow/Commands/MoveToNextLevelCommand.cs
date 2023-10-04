namespace Chillplay.OverHit.Base.Flow
{
    using LevelLoading;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class MoveToNextLevelCommand : LockableCommand
    {
        [Inject]
        public ILevelProvider LevelProvider { get; set; }
        
        public override void Execute()
        {
            LevelProvider.MoveToNext();
        }
    }
}