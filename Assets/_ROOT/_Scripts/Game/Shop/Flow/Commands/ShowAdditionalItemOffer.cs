namespace Chillplay.OverHit.Shop.Flow.Commands
{
    using System.Linq;
    using Chillplay.Shop;
    using UI;
    using Views;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class ShowAdditionalItemOffer : LockableCommand
    {
        [Inject] 
        public NotEnoughCurrencyForItem NotEnoughCurrencyForItem { get; set; }

        [Inject] 
        public SignalBus SignalBus { get; set; }
        [Inject] 
        public IShop Shop { get; set; }
        [Inject] 
        public IUIFactory UIFactory { get; set; }

        private ShopItemOfferPopUp itemOfferPopUp;
        private ShopItem additionalItem;
        private ShopItem mainItem;

        public override void Execute()
        {
            itemOfferPopUp = UIFactory.CreateWindow<ShopItemOfferPopUp>();
            
            additionalItem = NotEnoughCurrencyForItem.AdditionalItem;
            mainItem = NotEnoughCurrencyForItem.MainItem;

            itemOfferPopUp.Denied += OnOfferDenied;
            itemOfferPopUp.Initialize(NotEnoughCurrencyForItem.AdditionalItem);

            SignalBus.Subscribe<ItemBoughtSucceeded>(OnItemBoughtSucceeded);
        }

        private void OnOfferDenied()
        {
            itemOfferPopUp.Denied -= OnOfferDenied;
            SignalBus.TryUnsubscribe<ItemBoughtSucceeded>(OnItemBoughtSucceeded);
        }

        private void OnItemBoughtSucceeded(ItemBoughtSucceeded itemBoughtSucceeded)
        {
            if (itemBoughtSucceeded.Item == additionalItem)
            {
                SignalBus.TryUnsubscribe<ItemBoughtSucceeded>(OnItemBoughtSucceeded);

                var boughtPopUps = UIFactory.OpenedWindows.OfType<ShopItemBoughtPopUp>();
                boughtPopUps.Where(p => p.ShopItem == additionalItem).ToList().ForEach(p => p.Close());
                
                Shop.Buy(NotEnoughCurrencyForItem.MainItem);
            }
        }
    }
}