namespace Chillplay.OverHit.Shop.Factories
{
    using Group;
    using UnityEngine;
    using Zenject;

    public class ShopGroupViewsFactory : IFactory<ShopGroup, RectTransform, ShopGroupView>
    {
        private readonly DiContainer container;

        public ShopGroupViewsFactory(DiContainer container)
        {
            this.container = container;
        }
        
        public ShopGroupView Create(ShopGroup shopGroup, RectTransform parent)
        {
            var itemView = container
                .InstantiatePrefabForComponent<ShopGroupView>(shopGroup.ShopGroupViewPrefab, parent);

            return itemView;
        }
    }
}