namespace Chillplay.OverHit.Shop.Views
{
    using System;
    using Chillplay.Shop;
    using UI;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class ShopItemOfferPopUp : Window
    {
        public event Action Denied;
        
        [SerializeField] 
        private ShopItemView shopItemView;
        [SerializeField] 
        private Button buyButton;
        [SerializeField] 
        private Button closeButton;

        public void Initialize(ShopItem shopItem)
        {
            shopItemView.Initialize(shopItem);
            buyButton.onClick.AddListener(Close);
            closeButton.onClick.AddListener(OfferDenied);
        }

        private void OfferDenied()
        {
            Denied?.Invoke();
            Close();
        }
    }
}