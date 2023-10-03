namespace Chillplay.OverHit.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public void SetValue(float current, float max) =>
            image.fillAmount = current / max;

        public void SetValue(float ratio) =>
            image.fillAmount = ratio;
    }
}