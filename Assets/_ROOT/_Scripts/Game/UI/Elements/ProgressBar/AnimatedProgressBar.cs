namespace Chillplay.OverHit.UI
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public abstract class AnimatedProgressBar : MonoBehaviour
    {
        public float CurrentProgress;

        [Header("Values Settings")]
        [SerializeField]
        private float initialValue = 0.01f;

        [SerializeField]
        private float maximumValue = 1f;

        [Header("Animations Settings")]
        [SerializeField]
        private float updatingDuration = 0.5f;

        [SerializeField]
        private Ease updatingEase = Ease.InOutQuad;

        public event Action OnTargetValueReached;
        public event Action<float> OnProgressUpdated; 

        private float currentTargetValue;
        private Tween currentUpdateTween;

        public void Init(float initialNormalizedProgress = 0f)
        {
            currentUpdateTween?.Kill();
            SetCurrentProgressTo(initialNormalizedProgress);
        }

        public Tween UpdateProgressTo(float normalizedProgress)
        {
            return UpdateProgressTo(normalizedProgress, updatingDuration);
        }

        private Tween UpdateProgressTo(float normalizedProgress, float duration)
        {
            currentUpdateTween?.Kill();

            currentUpdateTween = DOTween
                .To(() => CurrentProgress, SetCurrentProgressTo, normalizedProgress, duration)
                .SetEase(updatingEase);

            currentTargetValue = normalizedProgress;

            return currentUpdateTween;
        }

        protected abstract void UpdateView(float normalizedValue);

        private void SetCurrentProgressTo(float normalizedProgress)
        {
            CurrentProgress = normalizedProgress;
            var viewValue = GetViewValue(normalizedProgress);
            UpdateView(viewValue);
            
            OnProgressUpdated?.Invoke(CurrentProgress);

            if (Mathf.Abs(CurrentProgress - currentTargetValue) < Constants.Epsilon)
            {
                OnTargetValueReached?.Invoke();
            }
        }

        private float GetViewValue(float normalizedProgress)
        {
            var viewValue = Mathf.InverseLerp(0, maximumValue, normalizedProgress);
            var initialAdaptedValue = Mathf.InverseLerp(0, maximumValue, initialValue);
            viewValue = initialAdaptedValue + (1f - initialAdaptedValue) * viewValue;

            return viewValue;
        }
    }
}