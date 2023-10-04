namespace Chillplay.OverHit.Shop.Wallet
{
    using System;
    using Utils;
    using Zenject;

    public class GemsWallet : IInGameCurrencyWallet<GemsCurrency>
    {
        [Inject]
        public GemsCurrency Currency { get; set; }

        public int Balance
        {
            get => balance.Value;
            private set
            {
                balance.Value = value;
                BalanceChanged?.Invoke();
            }
        }

        public event Action BalanceChanged;

        private readonly PlayerPrefsStoredValue<int> balance = new PlayerPrefsStoredValue<int>("GemsWallet", 1000);


        public void Put(int value)
        {
            if (value > 0)
            {
                Balance += value;
            }
        }

        public void Take(int value)
        {
            if (value > 0 && value <= Balance)
            {
                Balance -= value;
            }
        }
    }
}