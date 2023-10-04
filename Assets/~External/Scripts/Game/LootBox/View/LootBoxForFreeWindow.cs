namespace Chillplay.OverHit.LootBox.View
{
    using ChillPlay.LootBox;

    public class LootBoxForFreeWindow : LootBoxWindow
    {
        public override void Initialize(LootBox lootBox, LootBoxView lootBoxView)
        {
            base.Initialize(lootBox, lootBoxView);

            claimButton.onClick.AddListener(Claim);
            claimButton.onClick.AddListener(Close);
        }
        
        protected override void StartOpenSequence()
        {
            lootBoxView.Open(() =>
            {
                InitializeLootViews();
                ShowLootViews();
            });
        }
    }
}