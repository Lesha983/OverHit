namespace Chillplay.OverHit.Shop.Items
{
    using Chillplay.Shop;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(EpicChestItem), menuName = "Shop/Items/EpicChest", order = 0)]
    public class EpicChestItem : ShopItem
    {
        public override void BoughtSucceeded()
        {
            GiveBigReward();
        }

        public override void BoughtFailed()
        {
          
        }

        private void GiveBigReward()
        {
            
        }
    }
}