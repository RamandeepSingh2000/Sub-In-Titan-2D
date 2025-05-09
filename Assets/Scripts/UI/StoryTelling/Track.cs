using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Track : MonoBehaviour
{
    public float Duration;
    public UnityEvent OnTrackFadedIn;
    public UnityEvent OnTrackFadedOut;
    public bool IsPlayingAnimation { get; protected set; } = false;

    protected virtual void Awake()
    {
        Hide();
    }

    public abstract void Hide();

    public abstract void FadeIn(float duration, Action callback = null);

    public abstract void FadeOut(float duration, Action callback = null);
}
