namespace Chillplay.OverHit.Analytics
{
    using Core.Modules;

    [Module]
    public class GameAnalyticsInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameAnalyticEventsRouter>().AsSingle();
            Container.BindInterfacesTo<AzurGameEventsRouter>().AsSingle();
        }
    }
}
