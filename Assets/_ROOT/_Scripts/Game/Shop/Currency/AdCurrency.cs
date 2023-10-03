namespace Chillplay.OverHit.Shop
{
    using Chillplay.Shop;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(AdCurrency), menuName = "Shop/Currencies/AdCurrency", order = 0)]
    public class AdCurrency : Currency
    {
        public override string GetPriceTextFor(ShopItem shopItem)
        {
            return Name;
        }
    }
}