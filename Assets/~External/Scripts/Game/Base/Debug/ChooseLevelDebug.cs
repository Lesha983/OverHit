namespace Chillplay.Debug
{
    using DebugPanel;
    using OverHit.Base.Flow;
    using OverHit.Base.LevelLoading;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Utils;
    using Zenject;

    public class ChooseLevelDebug : MonoBehaviour
    {
        [Inject]
        private SignalBus SignalBus { get; set; }
        
        [Inject]
        private ILevelProvider LevelProvider { get; set; }

        [SerializeField]
        private TMP_InputField field;

        [SerializeField]
        private TMP_Text currentLevelNumber;

        [SerializeField]
        private Button moveButton;

        private int CurrentLevel
        {
            get => currentLevel.Value;
            set => currentLevel.Value = value;
        }

        private PlayerPrefsStoredValue<int> currentLevel;

        private void Start()
        {
            currentLevel = new PlayerPrefsStoredValue<int>("debug_level", 2);
            field.text = CurrentLevel.ToString();
            field.onEndEdit.AddListener(OnEndEdit);
            currentLevelNumber.text = $"Lvl: <b>{LevelProvider.CurrentLevelNumber}</b>";
            moveButton.onClick.AddListener(MoveTo);
        }

        private void MoveTo()
        {
            LevelProvider.MoveTo(CurrentLevel);
            SignalBus.AbstractFire<LevelRestarting>();
            var panel = FindObjectOfType<DebugPanel>();
            Destroy(panel.gameObject);
        }

        private void OnEndEdit(string value)
        {
            var level = int.Parse(value);
            CurrentLevel = Mathf.Max(level, 1);
            field.text = CurrentLevel.ToString();
        }
    }
}