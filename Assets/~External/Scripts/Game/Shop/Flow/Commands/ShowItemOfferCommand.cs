namespace Chillplay.OverHit.Shop.Flow.Commands
{
    using Chillplay.UI;
    using Views;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class ShowItemOfferCommand : LockableCommand
    {
        [Inject] 
        public ShowItemOffer ShowItemOffer { get; set; }
        [Inject] 
        public IUIFactory UIFactory { get; set; }
        
        public override void Execute()
        {
            var itemOfferPopUp = UIFactory.CreateWindow<ShopItemOfferPopUp>();
            itemOfferPopUp.Initialize(ShowItemOffer.Item);
        }
    }
}