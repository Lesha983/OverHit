namespace Chillplay.OverHit.UI.Fader
{
    using System;
    using Bellatrix.Utils;
    using Chillplay.UI.Fader;
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaFader : UIFader
    {
        private CanvasGroup canvasGroup;
        
        [SerializeField, Range(0.1f, 3f)]
        private float duration = 1f;

        [SerializeField] 
        private Ease ease = Ease.Linear;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void FadeIn(Action onFinished = null)
        {
            canvasGroup
                .FadeIn(duration)
                .SetEase(ease)
                .OnComplete(() => onFinished?.Invoke());
        }

        public override void FadeOut(Action onFinished = null)
        {
            canvasGroup
                .FadeOut(duration)
                .SetEase(ease)
                .OnComplete(() => onFinished?.Invoke());
        }
    }
}