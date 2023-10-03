namespace Chillplay.OverHit.Shop
{
    using System.Collections.Generic;
    using System.Linq;
    using Chillplay.Shop;
    using Flow;
    using Items;
    using Wallet;
    using Zenject;

    public abstract class InGameCurrency<T> : Currency where T : InGameCurrency<T>
    {
        [Inject] 
        public IInGameCurrencyWallet<T> CurrencyWallet { get; set; }
        [Inject]
        public SignalBus SignalBus { get; set; }
        
        public List<InGameCurrencyItem<T>> Items;

        public virtual void OnCurrencyNotEnough(ShopItem item)
        {
            int price = (int) item.CurrentPricing.Price;
            if (price > CurrencyWallet.Balance)
            {
                var deficit = price - CurrencyWallet.Balance;

                var additionalItem = Items
                    .OrderBy(i => i.RewardCount)
                    .FirstOrDefault(i => i.RewardCount >= deficit && i.Available);

                if (additionalItem != null)
                {
                    SignalBus.Fire(new NotEnoughCurrencyForItem(item,additionalItem));
                }
            }
        }
    }
}