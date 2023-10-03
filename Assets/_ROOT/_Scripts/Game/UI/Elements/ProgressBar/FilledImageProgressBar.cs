namespace Chillplay.OverHit.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class FilledImageProgressBar : AnimatedProgressBar
    {
        [SerializeField]
        private Image progressLine;
        
        protected override void UpdateView(float normalizedValue)
        {
            progressLine.fillAmount = normalizedValue;
        }
    }
}