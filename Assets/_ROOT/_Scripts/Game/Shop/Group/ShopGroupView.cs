namespace Chillplay.OverHit.Shop.Group
{
    using System.Collections.Generic;
    using Factories;
    using UnityEngine;
    using Views;
    using Zenject;

    public class ShopGroupView : MonoBehaviour
    {
        public List<ShopItemView> ItemViews;

        [SerializeField] 
        private RectTransform itemsParent;
        
        private ShopItemViewsFactory itemViewsFactory;

        [Inject]
        public void Construct(ShopItemViewsFactory itemViewsFactory)
        {
            ItemViews = new List<ShopItemView>();
            this.itemViewsFactory = itemViewsFactory;
        }

        public void Initialize(ShopGroup shopGroup)
        {
            foreach (var shopItem in shopGroup.ShopItems)
            {
                if(shopItem.BoughtLimitExhausted)
                    continue;
                
                var item = itemViewsFactory.Create(shopItem, shopGroup.ShopItemViewPrefab, itemsParent);
                item.Initialize(shopItem);
                ItemViews.Add(item);
            }
        }
    }
}