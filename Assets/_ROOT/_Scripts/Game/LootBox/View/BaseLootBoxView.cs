namespace Chillplay.OverHit.LootBox.View
{
    using System;
    using ChillPlay.LootBox;
    using DG.Tweening;
    using UnityEngine;

    public class BaseLootBoxView : LootBoxView
    {
        public float BounceAnimationScaleSmall;
        public float BounceAnimationScaleLarge;
        public float BounceAnimationDuration;
        public Ease BounceAnimationEase;
        
        public override void Open(Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();
            transform.localScale = Vector3.zero;
            sequence
                .Append(transform.DOScale(1f, BounceAnimationDuration/2).SetEase(BounceAnimationEase))
                .Append(Bounce(transform, 1f))
                .OnComplete(() => callback?.Invoke());
        }

        public override void Hide(Action callback = null)
        {
            transform.DOScale(0f, BounceAnimationDuration)
                .SetEase(BounceAnimationEase).OnComplete(() =>
                {
                    callback?.Invoke();
                    Destroy(gameObject);
                });
        }

        public Sequence Bounce(Transform transform, float scale)
        {
            float initialScale = scale;
            float scaleSmall = BounceAnimationScaleSmall;
            float scaleLarge = BounceAnimationScaleLarge;
            float scaleDuration = BounceAnimationDuration;
            Ease ease = BounceAnimationEase;

            return DOTween.Sequence()
                .Join(transform.DOScale(initialScale * scaleSmall, scaleDuration).SetEase(ease))
                .Append(transform.DOScale(initialScale * scaleLarge, scaleDuration).SetEase(ease))
                .Append(transform.DOScale(initialScale, scaleDuration).SetEase(ease));
        }
    }
}