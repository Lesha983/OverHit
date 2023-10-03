namespace Chillplay.OverHit.Shop
{
    using System.Collections.Generic;
    using System.Linq;
    using Purchasing;
    using Chillplay.Shop;
    using UnityEngine.Purchasing;
    using Zenject;

    public class RealCurrencyBuyer : Buyer<RealCurrency>, IInitializable
    {
        private readonly IPurchaser purchaser;
        private readonly ShopItemsStorage shopItemsStorage;
        private readonly IPurchasingInitializer initializer;
        
        private List<ShopItem> items;

        public RealCurrencyBuyer(IPurchaser purchaser, SignalBus signalBus,
            ShopItemsStorage shopItemsStorage, IPurchasingInitializer initializer)
        {
            this.purchaser = purchaser;
            this.shopItemsStorage = shopItemsStorage;
            this.initializer = initializer;
            purchaser.PurchaseValidated += OnItemPurchased;
            purchaser.Initialized += OnPurchaserInitialized;
        }

        private void OnPurchaserInitialized()
        {
            
        }

        public void Initialize()
        {
            items = shopItemsStorage.ItemsFor(currency);
            initializer.Initialize(items.ToArray());
        }

        public override void Buy(ShopItem item)
        {
            if(CanBuy(item))
                purchaser.StartPurchase(item.Id);
            else
                OnItemBoughtFailed(item);
        }

        public override bool CanBuy(ShopItem item)
        {
            return purchaser.Products.ContainsKey(item.Id) && item.Available;
        }

        private void OnItemPurchased(PurchaseEventArgs args)
        {
            ShopItem item = shopItemsStorage.ItemFor(args.purchasedProduct.definition.id);

            if (item != null)
            {
                OnItemBoughtSucceeded(item);
            }
        }
    }
}