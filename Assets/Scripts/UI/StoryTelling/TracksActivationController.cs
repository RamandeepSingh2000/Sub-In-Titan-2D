using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public abstract class TracksActivationController : MonoBehaviour
{    
    [SerializeField] protected Track[] Tracks;
    [SerializeField] protected float FadeInTime;
    [SerializeField] protected float FadeOutTime;
    [SerializeField] protected bool PlayOnStart;
    [SerializeField] protected float PlayDelay = 0.0f;
    [SerializeField] protected bool DeactivateLastTrack = true;

    public UnityEvent OnSequenceStarted;
    public UnityEvent OnSequenceFinished;    

    private void Start()
    {
        if (PlayOnStart)
            Invoke(nameof(StartSequence), PlayDelay);
    }

    public virtual void StartSequence()
    {
        OnSequenceStarted?.Invoke();
    }

    //protected void FadeInTrack(Track track, Action callback = null)
    //{
    //    //track.Object.SetActive(true);
    //    //DOTween.To(() => CanvasGroupRef.alpha, x => CanvasGroupRef.alpha = x, 1f, FadeInTime)
    //    //    .onComplete += () =>
    //    //    {
    //    //        track.OnTrackPlayed?.Invoke();
    //    //        callback?.Invoke();
    //    //    };
    //}

    //protected void FadeOutTrack(Track track, Action callback = null)
    //{
    //    //DOTween.To(() => CanvasGroupRef.alpha, x => CanvasGroupRef.alpha = x, 0f, FadeOutTime)
    //    //    .onComplete += () =>
    //    //    {
    //    //        track.Object.SetActive(false);
    //    //        callback?.Invoke();
    //    //    };
    //}

}
