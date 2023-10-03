namespace Chillplay.OverHit.Purchasing
{
    using Chillplay.Purchasing;
    using UnityEngine.Purchasing;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class InitializePurchasingCommand : LockableCommand
    {
        [Inject] 
        public IPurchasingInitializer PurchasingInitializer { get; set; }

        private readonly IProduct[] products = 
        {
            new Product() {Id = "Item1", ProductType = ProductType.Consumable},
            new Product() {Id = "Item2", ProductType = ProductType.Consumable},
        };
        
        public override void Execute()
        {
            PurchasingInitializer.Initialize(products);
        }
    }
}