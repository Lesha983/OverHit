namespace Chillplay.OverHit.Shop
{
    using Chillplay.Shop;
    using Wallet;
    using Zenject;

    public class GemsCurrencyBuyer : Buyer<GemsCurrency>
    {
        [Inject] 
        public IInGameCurrencyWallet<GemsCurrency> Wallet { get; set; }
        
        public override void Buy(ShopItem item)
        {
            if (CanBuy(item))
            {
                OnItemBoughtSucceeded(item);
                Wallet.Take((int) item.CurrentPricing.Price);
            }
            else
            {
                OnItemBoughtFailed(item);
                Wallet.Currency.OnCurrencyNotEnough(item);
            }
        }

        public override bool CanBuy(ShopItem item)
        {
            return Wallet.Balance >= (int) item.CurrentPricing.Price;
        }
    }
}