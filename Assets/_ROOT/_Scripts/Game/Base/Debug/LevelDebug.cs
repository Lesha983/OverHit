namespace Chillplay.Debug
{
    using OverHit.Base.Flow;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class LevelDebug : MonoBehaviour
    {
        [Inject]
        private SignalBus SignalBus { get; set; }

        [SerializeField]
        private Button failButton;

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button completeButton;

        private void Start()
        {
            failButton.onClick.AddListener(Fail);
            restartButton.onClick.AddListener(Restart);
            completeButton.onClick.AddListener(Complete);
        }

        private void Fail()
        {
            SignalBus.AbstractFire<LevelFailing>();
        }

        private void Restart()
        {
            SignalBus.AbstractFire<LevelRestarting>();
        }

        private void Complete()
        {
            SignalBus.AbstractFire<LevelCompleting>();
        }
    }
}