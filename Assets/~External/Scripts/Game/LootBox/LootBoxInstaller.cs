namespace Chillplay.OverHit.LootBox
{
    using Core.Modules;
    using View;
    using ChillPlay.LootBox;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    [Module]
    public class LootBoxInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            Container.BindSignalToCommand<ReceiveLootBox>()
                .To<SwitchToLootBoxCamera>()
                .To<CreateLootBoxWindow>();
            
            Container.BindInterfacesTo<Provider<LootBoxViewParent>>().AsSingle();
        }
    }
}