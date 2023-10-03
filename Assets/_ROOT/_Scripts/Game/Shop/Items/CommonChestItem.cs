namespace Chillplay.OverHit.Shop.Items
{
    using Chillplay.Shop;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(CommonChestItem), menuName = "Shop/Items/CommonChest", order = 0)]
    public class CommonChestItem : ShopItem
    {
        public override void BoughtSucceeded()
        { 
            GiveSmallReward();
        }

        public override void BoughtFailed()
        {
        }

        private void GiveSmallReward()
        {
            
        }
    }
}