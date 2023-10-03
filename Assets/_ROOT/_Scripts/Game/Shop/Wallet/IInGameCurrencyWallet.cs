namespace Chillplay.OverHit.Shop.Wallet
{
    using System;

    public interface IInGameCurrencyWallet<out T> where T : InGameCurrency<T>
    {
        T Currency { get; }
        int Balance { get; }
        void Put(int value);

        void Take(int value);
        event Action BalanceChanged;
    }
}