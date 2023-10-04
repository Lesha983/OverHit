namespace Chillplay.OverHit.Shop.Factories
{
    using Chillplay.Shop;
    using UnityEngine;
    using Views;
    using Zenject;

    public class ShopItemViewsFactory : IFactory<ShopItem, ShopItemView, RectTransform, ShopItemView>
    {
        private readonly DiContainer container;

        public ShopItemViewsFactory(DiContainer container)
        {
            this.container = container;
        }
        
        public ShopItemView Create(ShopItem shopItem, ShopItemView view, RectTransform parent)
        {
            var itemView = container
                .InstantiatePrefabForComponent<ShopItemView>(view, parent);

            return itemView;
        }
    }
}