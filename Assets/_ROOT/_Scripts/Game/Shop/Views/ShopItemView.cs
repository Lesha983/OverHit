namespace Chillplay.OverHit.Shop.Views
{
    using System;
    using Chillplay.Shop;
    using Purchasing;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text name;
        [SerializeField] 
        private TMP_Text price;
        
        [SerializeField] 
        private TMP_Text counterText;
        [SerializeField] 
        private GameObject counter;
        [SerializeField] 
        private TMP_Text timer;

        [SerializeField] 
        private Image icon;
        [SerializeField] 
        private Button button;
        [SerializeField]
        private bool hideButton = true;

        [SerializeField]
        private ShopItem shopItem;
        private IShop shop;
        private IPurchaser purchaser;

        [Inject]
        public void Construct(IShop shop, IPurchaser purchaser)
        {
            this.purchaser = purchaser;
            this.shop = shop;
        }

        public void Initialize(ShopItem shopItem)
        {
            this.shopItem = shopItem;
            name.SetText(shopItem.name);
            icon.sprite = shopItem.Icon;
            button.onClick.AddListener(BuyItem);
            
            shopItem.PricingsChanged += UpdateView;
            shopItem.ItemBoughtSucceeded += UpdateView;
            shopItem.ItemBoughtFailed += UpdateView;
            UpdateView();
        }

        public void BuyItem()
        {
            shop.Buy(shopItem);
        }

        private void UpdateView()
        {
            button.interactable = !shopItem.BoughtLimitExhausted && shopItem.Available;
            if(hideButton)
                button.gameObject.SetActive(shopItem.Available);

            if (shopItem.CurrentPricing != null)
            {
                string priceText = shopItem.CurrentCurrency.GetPriceTextFor(shopItem);

                price.SetText(priceText);
                counter.SetActive(shopItem.CurrentPricing.LimitedQuantity);
                counterText.SetText(shopItem.CurrentPricing.CurrentStack.ToString());
            }
        }

        private void Update()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            string text = String.Empty;
            
            if (shopItem.ReloadingPricing?.NextStackAvailableAt > 0)
            {
                text = 
                    shopItem.ReloadingPricing.NextStackAvailableAtTimeSpan
                        .ToString("hh\\:mm\\:ss");
            }
            
            timer.SetText(text);
        }

        private void OnDestroy()
        {
            shopItem.PricingsChanged -= UpdateView;
            shopItem.ItemBoughtSucceeded -= UpdateView;
            shopItem.ItemBoughtFailed -= UpdateView;
        }
    }
}