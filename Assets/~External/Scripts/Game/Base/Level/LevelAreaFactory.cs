namespace Chillplay.OverHit.Base.LevelLoading
{
    using OverHit.Level;
    using UnityEngine;
    using Zenject;

    public class LevelAreaFactory : IFactory<LevelArea, Transform, LevelArea>
    {
        private readonly DiContainer diContainer;

        public LevelAreaFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }
        
        public LevelArea Create(LevelArea prefab, Transform parent)
        {
            return diContainer.InstantiatePrefabForComponent<LevelArea>(prefab, parent);
        }
    }
}