namespace Chillplay.OverHit
{
    using Camera;
    using Chillplay.Sounds.General;
    using Core.Modules;
    using Sounds;
    using Time;
    using Zenject;
    using Zenject.Extensions.Lazy;

    [Module]
    public class OverHitInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeCounter>().To<TimeCounter>().AsTransient();
            Container.BindInterfacesTo<TimeCounterService>().AsSingle();
            Container.BindInterfacesTo<Provider<GameCamerasService>>().AsSingle();

            Container
                .Bind(typeof(GameSounds), typeof(OverHitSounds), typeof(IInitializable))
                .To<OverHitSounds>().AsSingle();
        }
    }
}