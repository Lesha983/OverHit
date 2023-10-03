namespace Chillplay.OverHit.Shop.Items
{
    using Chillplay.Shop;
    using Wallet;
    using Zenject;

    public abstract class InGameCurrencyItem<T> : ShopItem where T : InGameCurrency<T>
    {
        [Inject] 
        public IInGameCurrencyWallet<T> Wallet { get; set; }
        
        public int RewardCount;
        
        public override void BoughtSucceeded()
        {
            GiveReward();
        }
        
        public override void BoughtFailed()
        {
            
        }

        protected virtual void GiveReward()
        {
            Wallet.Put(RewardCount);
        }
    }
}