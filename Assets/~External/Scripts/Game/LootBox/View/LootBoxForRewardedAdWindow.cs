namespace Chillplay.OverHit.LootBox.View
{
    using Chillplay.Ads;
    using ChillPlay.LootBox;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class LootBoxForRewardedAdWindow : LootBoxWindow
    {
        [Inject] 
        public IAdvertiser Advertiser { get; set; }
        
        [SerializeField] 
        private Button refuseButton;
        [SerializeField] 
        private float delayBeforeRefuseButton;
        
        private string Placement = "LootBox";

        public override void Initialize(LootBox lootBox, LootBoxView lootBoxView)
        {
            base.Initialize(lootBox, lootBoxView);
            
            refuseButton.onClick.AddListener(Close);
            claimButton.onClick.AddListener(WatchAd);
        }

        protected override void StartOpenSequence()
        {
            lootBoxView.Open(() =>
            {
                InitializeLootViews();
                HideLootViews();
            });
        }

        protected override void ShowButtons()
        {
            base.ShowButtons();

            DOVirtual.DelayedCall(delayBeforeRefuseButton, 
                () => refuseButton.gameObject.SetActive(true));
        }

        protected override void HideButtons()
        {
            base.HideButtons();
            refuseButton.gameObject.SetActive(false);
        }

        private async void WatchAd()
        {
            if (Advertiser.HasRewardedVideoAndNotCapped(Placement))
            {
                var result = await
                    Advertiser.ShowRewardedAd(Placement);

                if (result == AdWatchResult.Watched)
                {
                    ShowLootViews();
                    claimButton.onClick.RemoveListener(WatchAd);
                    claimButton.onClick.AddListener(Claim);
                    claimButton.onClick.AddListener(Close);
                    UpdateButtonsView();
                }
            }
        }

        private void UpdateButtonsView()
        {
            claimButton.GetComponentInChildren<TMP_Text>().SetText("Claim");
            refuseButton.gameObject.SetActive(false);
        }
    }
}
