namespace Chillplay.OverHit.Shop
{
    using Chillplay.Shop;
    using Chillplay.UI.Flow.Commands;
    using Core.Modules;
    using Factories;
    using Flow;
    using Flow.Commands;
    using Views;
    using Wallet;
    using Zenject;
    using Zenject.Extensions.Commands;

    [Module]
    public class OverHitShopInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<SwitchToShop>();
            Container.DeclareSignal<ShowItemOffer>();
            Container.DeclareSignal<NotEnoughCurrencyForItem>();

            Container.BindSignalToCommand<SwitchToShop>()
                .To<CreateWindow<ShopWindow>>();

            Container.BindSignalToCommand<ItemBoughtSucceeded>()
                .To<ShowItemBoughtCommand>();

            Container.BindSignalToCommand<ShowItemOffer>()
                .To<ShowItemOfferCommand>();

            Container.BindSignalToCommand<NotEnoughCurrencyForItem>()
                .To<ShowAdditionalItemOffer>();
            
            Container.BindInterfacesAndSelfTo<ShopItemViewsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShopGroupViewsFactory>().AsSingle();

            Container.BindInterfacesTo<GemsWallet>().AsSingle();
            Container.BindInterfacesTo<GoldWallet>().AsSingle();

            Container.BindInterfacesTo<AdCurrencyBuyer>().AsSingle();
            Container.BindInterfacesTo<RealCurrencyBuyer>().AsSingle();
            Container.BindInterfacesTo<GemsCurrencyBuyer>().AsSingle();
            Container.BindInterfacesTo<GoldCurrencyBuyer>().AsSingle();
        }
    }
}