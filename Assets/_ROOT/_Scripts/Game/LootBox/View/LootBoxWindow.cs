namespace Chillplay.OverHit.LootBox.View
{
    using System.Collections.Generic;
    using ChillPlay.LootBox;
    using Chillplay.UI;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class LootBoxWindow : Window
    {
        [SerializeField] protected Button claimButton;

        protected LootBoxView lootBoxView;

        [SerializeField] private float delay;
        [SerializeField] private TMP_Text lootBoxName;
        [SerializeField] private RectTransform lootParent;
        [SerializeField] private LootView lootViewPrefab;

        private LootBox lootBox;
        private List<LootResult> lootResults;
        private List<LootView> lootViews;

        public virtual void Initialize(LootBox lootBox, LootBoxView lootBoxView)
        {
            this.lootBoxView = lootBoxView;
            this.lootBox = lootBox;
            lootBoxName.SetText(lootBox.name);
            OpenLootBox();

            HideButtons();
            StartOpenSequence();
        }

        protected abstract void StartOpenSequence();

        protected void InitializeLootViews()
        {
            lootViews = new List<LootView>();
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(delay);

            foreach (var lootResult in lootResults)
            {
                var lootView = Instantiate(lootViewPrefab, lootParent);
                lootViews.Add(lootView);
                lootView.Initialize(lootResult);
                lootView.transform.localScale = Vector3.zero;
                sequence.Append(lootView.transform.DOScale(1f, 0.5f));
            }

            sequence.OnComplete(() => lootBoxView.Hide(ShowButtons));
        }

        protected void Claim()
        {
            lootBox.Claim(lootResults);
        }

        protected void ShowLootViews()
        {
            lootViews.ForEach(l => l.Show());
        }

        protected void HideLootViews()
        {
            lootViews.ForEach(l => l.Hide());
        }

        private void OpenLootBox()
        {
            lootResults = lootBox.Open();
        }

        protected virtual void ShowButtons()
        {
            claimButton.gameObject.SetActive(true);
        }

        protected virtual void HideButtons()
        {
            claimButton.gameObject.SetActive(false);
        }
    }
}