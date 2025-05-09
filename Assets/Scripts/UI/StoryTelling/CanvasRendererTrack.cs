using DG.Tweening;
using System;
using UnityEngine;

public class CanvasRendererTrack : Track
{
    [SerializeField] private CanvasRenderer _canvasRenderer;

    public override void FadeIn(float duration, Action callback = null)
    {
        IsPlayingAnimation = true;
        DOTween.To(_canvasRenderer.GetAlpha, _canvasRenderer.SetAlpha, 1f, duration)
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
        DOTween.To(_canvasRenderer.GetAlpha, _canvasRenderer.SetAlpha, 0f, duration)
            .onComplete += () =>
            {
                IsPlayingAnimation = false;
                OnTrackFadedOut?.Invoke();
                callback?.Invoke();
            };
    }

    public override void Hide()
    {
        _canvasRenderer.SetAlpha(0);
    }
}
