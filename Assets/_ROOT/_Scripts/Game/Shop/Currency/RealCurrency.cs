namespace Chillplay.OverHit.Shop
{
    using Chillplay.Shop;
    using Purchasing;
    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = nameof(RealCurrency), menuName = "Shop/Currencies/RealCurrency", order = 0)]
    public class RealCurrency : Currency
    {
        [Inject] 
        public IPurchaser Purchaser { get; set; }
        
        public override string GetPriceTextFor(ShopItem shopItem)
        {
            return Purchaser.Products.TryGetValue(shopItem.Id, out var product) ? 
                product.metadata.localizedPriceString : 
                base.GetPriceTextFor(shopItem);
        }
    }
}