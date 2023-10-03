namespace Chillplay.OverHit.UI
{
    using Base.LevelLoading;
    using TMPro;
    using UnityEngine;
    using Zenject;

    public class LevelNumber : MonoBehaviour
    {
        [Inject] 
        public ILevelProvider LevelProvider { get; set; }

        [SerializeField] 
        private TMP_Text _text;

        private void Start()
        {
            _text.SetText($"Level {LevelProvider.CurrentLevelNumber}");
        }
    }
}