namespace Chillplay.OverHit.Shop.Items
{
    using Chillplay.Shop;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(LegendaryChestItem), menuName = "Shop/Items/LegendaryChest", order = 0)]
    public class LegendaryChestItem : ShopItem
    {
        public override void BoughtSucceeded()
        {
            GiveUnbelievableReward();
        }

        public override void BoughtFailed()
        {
           
        }

        private void GiveUnbelievableReward()
        {
            
        }
    }
}