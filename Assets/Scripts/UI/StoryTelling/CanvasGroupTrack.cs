using DG.Tweening;
using System;
using UnityEngine;

public class CanvasGroupTrack : Track
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public override void FadeIn(float duration, Action callback = null)
    {
        IsPlayingAnimation = true;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, duration)
            .onComplete += () =>
            {
                IsPlayingAnimation = false;
                callback?.Invoke();
                OnTrackFadedIn?.Invoke();
            };
    }

    public override void FadeOut(float duration, Action callback = null)
    {
        IsPlayingAnimation = true;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, duration)
            .onComplete += () =>
            {
                IsPlayingAnimation = false;
                OnTrackFadedOut?.Invoke();
                callback?.Invoke();
            };
    }

    public override void Hide()
    {
        _canvasGroup.alpha = 0;
    }
}
