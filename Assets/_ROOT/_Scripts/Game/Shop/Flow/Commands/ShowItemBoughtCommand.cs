namespace Chillplay.OverHit.Shop.Flow.Commands
{
    using Chillplay.Shop;
    using Chillplay.UI;
    using Views;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class ShowItemBoughtCommand : LockableCommand
    {
        [Inject] 
        public IUIFactory UIFactory { get; set; }
        
        [Inject] 
        public ItemBoughtSucceeded ItemBoughtSucceeded { get; set; }
        
        public override void Execute()
        {
            UIFactory.CreateWindow<ShopItemBoughtPopUp>().Initialize(ItemBoughtSucceeded.Item);
        }
    }
}