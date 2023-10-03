namespace Chillplay.OverHit.Shop.Views
{
    using System;
    using TMPro;
    using UnityEngine;
    using Wallet;
    using Zenject;

    public abstract class GameCurrencyBalanceView<T> : MonoBehaviour where T : InGameCurrency<T>
    {
        [Inject] 
        public IInGameCurrencyWallet<T> Wallet { get; set; }

        [SerializeField]
        private TMP_Text text;

        private void Start()
        {
            Wallet.BalanceChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
            text.SetText($"{Wallet.Currency.Name}{Wallet.Balance.ToString()}");
        }

        private void OnDestroy()
        {
            Wallet.BalanceChanged -= UpdateView; 
        }
    }
}