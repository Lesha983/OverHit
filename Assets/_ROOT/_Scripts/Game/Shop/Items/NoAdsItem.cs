namespace Chillplay.OverHit.Shop.Items
{
    using Chillplay.Ads;
    using Chillplay.Shop;
    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = nameof(NoAdsItem), menuName = "Shop/Items/NoAds", order = 0)]
    public class NoAdsItem : ShopItem
    {
        [Inject] 
        public IAdDisabler AdDisabler { get; set; }
        
        
        public override void BoughtSucceeded()
        {
            AdDisabler.RemoveAds();
        }

        public override void BoughtFailed()
        {

        }
    }
}