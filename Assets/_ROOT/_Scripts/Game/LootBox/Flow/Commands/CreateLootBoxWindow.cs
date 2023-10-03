namespace Chillplay.OverHit.LootBox
{
    using ChillPlay.LootBox;
    using Chillplay.UI;
    using View;
    using Zenject;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    public class CreateLootBoxWindow : LockableCommand
    {
        [Inject] 
        public ReceiveLootBox ReceiveLootBox { get; set; }

        [Inject]
        public IUIFactory UIFactory { get; set; }
        [Inject] 
        public IProvider<LootBoxViewParent> LootBoxViewParent { get; set; }
        
        public override void Execute()
        {
            LootBoxWindow window;
            if (ReceiveLootBox.ForRewarded)
            {
                window = UIFactory.CreateWindow<LootBoxForRewardedAdWindow>(UIRootType.Camera);
            }
            else
            {
                window = UIFactory.CreateWindow<LootBoxForFreeWindow>(UIRootType.Camera);
            }
             
            LootBoxViewParent.Instance.CreateLootBox(ReceiveLootBox.LootBox.View);
            window.Initialize(ReceiveLootBox.LootBox, LootBoxViewParent.Instance.LootBoxViewInstance);
        }
    }
}