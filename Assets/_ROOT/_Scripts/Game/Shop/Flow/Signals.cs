namespace Chillplay.OverHit.Shop.Flow
{
    using Chillplay.Shop;

    public class SwitchToShop
    {
        
    }

    public class ShowItemOffer
    {
        public ShopItem Item { get; }

        public ShowItemOffer(ShopItem item)
        {
            Item = item;
        }
    }

    public class NotEnoughCurrencyForItem
    {
        public ShopItem MainItem { get; }
        public ShopItem AdditionalItem { get; }

        public NotEnoughCurrencyForItem(ShopItem mainItem, ShopItem additionalItem)
        {
            MainItem = mainItem;
            AdditionalItem = additionalItem;
        }
    }
}