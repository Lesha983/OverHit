namespace Chillplay.OverHit.Shop
{
    using Ads;
    using Chillplay.Shop;

    public class AdCurrencyBuyer : Buyer<AdCurrency>
    {
        private const string Placement = "Shop";
        private readonly IAdvertiser advertiser;

        public AdCurrencyBuyer(IAdvertiser advertiser)
        {
            this.advertiser = advertiser;
        }

        public override async void Buy(ShopItem item)
        {
            if (CanBuy(item))
            {
                var result = await advertiser.ShowRewardedAd(Placement);

                if (result == AdWatchResult.Watched)
                {
                    OnItemBoughtSucceeded(item);
                }
                else
                {
                    OnItemBoughtFailed(item);
                }
            }
            else
            {
                OnItemBoughtFailed(item);
            }
        }

        public override bool CanBuy(ShopItem item)
        {
            return advertiser.HasRewardedVideoAndNotCapped(Placement);
        }
    }
}