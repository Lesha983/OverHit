namespace Chillplay.OverHit.Base.LevelLoading
{
    using Cysharp.Threading.Tasks;
    using OverHit.Level;
    using Zenject.Extensions.Lazy;

    public interface ILevelInstanceParent
    {
        LevelArea Level { get; }
        UniTask SetLevel(LevelConfig levelConfig);
        UniTask TryCleanUp();
    }

    public abstract class LevelInstanceParent<T> : MonoProvided<LevelInstanceParent<T>, ILevelInstanceParent>, ILevelInstanceParent  where T : LevelConfig 
    {
        public LevelArea Level { get; protected set; }
        public UniTask SetLevel(LevelConfig levelConfig)
        {
            return SetLevel(levelConfig as T);
        }

        protected abstract UniTask SetLevel(T levelConfig);

        public abstract UniTask TryCleanUp();
    }
}