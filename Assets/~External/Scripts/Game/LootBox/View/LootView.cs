namespace Chillplay.OverHit.LootBox.View
{
    using System.Globalization;
    using ChillPlay.LootBox;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LootView : MonoBehaviour
    {
        [SerializeField] 
        private Image icon;
        [SerializeField] 
        private Image Blind;
        [SerializeField]
        private TMP_Text value;

        public void Initialize(LootResult lootResult)
        {
            icon.sprite = lootResult.LootType.Icon;
            value.SetText(lootResult.Count.ToString(CultureInfo.InvariantCulture));
        }

        public void Show()
        {
            Blind.gameObject.SetActive(false);
        }

        public void Hide()
        {
            Blind.gameObject.SetActive(true);
        }
    }
}