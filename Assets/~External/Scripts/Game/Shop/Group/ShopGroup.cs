namespace Chillplay.OverHit.Shop.Group
{
    using System.Collections.Generic;
    using Chillplay.Shop;
    using UnityEngine;
    using Views;

    [CreateAssetMenu(fileName = nameof(ShopGroup), menuName = "Shop/Group", order = 0)]
    public class ShopGroup : ScriptableObject
    {
        public ShopItemView ShopItemViewPrefab;
        public ShopGroupView ShopGroupViewPrefab;
        public List<ShopItem> ShopItems;
    }
}