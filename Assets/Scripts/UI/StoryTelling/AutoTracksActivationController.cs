using System.Collections;
using UnityEngine;

public class AutoTracksActivationController : TracksActivationController
{
    private byte _currentIndex = 0;

    public override void StartSequence()
    {
        base.StartSequence();
        StartCoroutine(PlaySequenceCoroutine());
    }

    private IEnumerator PlaySequenceCoroutine()
    {
        while (_currentIndex < Tracks.Length)
        {
            yield return StartCoroutine(PlayTrack(Tracks[_currentIndex]));
            _currentIndex++;
        }
        OnSequenceFinished?.Invoke();
    }

    private IEnumerator PlayTrack(Track track)
    {
        track.gameObject.SetActive(true);
        track.FadeIn(FadeInTime);
        yield return new WaitForSeconds(track.Duration + FadeInTime);
        if (DeactivateLastTrack)
        {
            track.FadeOut(FadeOutTime);
            yield return new WaitForSeconds(FadeOutTime);
            track.gameObject.SetActive(false);
        }
    }
}
