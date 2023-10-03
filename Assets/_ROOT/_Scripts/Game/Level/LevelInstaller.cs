namespace Chillplay.OverHit.Level
{
    using UnityEngine;
    using Zenject;

    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelArea levelArea;
        
        public override void InstallBindings()
        {
            Container.BindInstance(levelArea).AsSingle();
        }
    }
}