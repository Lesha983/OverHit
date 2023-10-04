namespace Chillplay.OverHit.Shop.Items
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(GoldItem), menuName = "Shop/Items/Gold", order = 0)]
    public class GoldItem : InGameCurrencyItem<GoldCurrency>
    {
        
    }
}