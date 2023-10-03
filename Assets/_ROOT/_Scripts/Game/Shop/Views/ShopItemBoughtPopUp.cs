namespace Chillplay.OverHit.Shop.Views
{
    using Chillplay.Shop;
    using Chillplay.UI;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopItemBoughtPopUp : Window
    {
        public ShopItem ShopItem { get; private set; }
        
        [SerializeField] 
        private Image icon;
        [SerializeField] 
        private TMP_Text name;

        public void Initialize(ShopItem shopItem)
        {
            icon.sprite = shopItem.Icon;
            name.SetText(shopItem.name);
            ShopItem = shopItem;
        }
    }
}