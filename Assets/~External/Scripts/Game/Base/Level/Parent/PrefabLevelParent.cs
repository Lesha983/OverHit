namespace Chillplay.OverHit.Base.LevelLoading
{
    using Cysharp.Threading.Tasks;
    using OverHit.Level;
    using Zenject;

    public class PrefabLevelParent : LevelInstanceParent<PrefabLevelConfig>
    {
        [Inject] 
        public LevelAreaFactory LevelAreaFactory { get; set; }

        protected override UniTask SetLevel(PrefabLevelConfig levelConfig)
        {
            TryCleanUp();
            Level = LevelAreaFactory.Create(levelConfig.LevelArea, transform);
            
            return UniTask.CompletedTask;
        }

        public override UniTask TryCleanUp()
        {
            if (Level != null)
            {
                Destroy(Level.gameObject);
            }
            
            return UniTask.CompletedTask;
        }
    }
}