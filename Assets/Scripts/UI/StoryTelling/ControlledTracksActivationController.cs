using System;
using UnityEngine;
using UnityEngine.Events;

public class ControlledTracksActivationController : TracksActivationController
{
    [Space]
    [SerializeField] private GameObject _previousButton;
    [SerializeField] private GameObject _nextButton;

    public UnityEvent OnCurrentTrackFadedIn;
    public UnityEvent OnCurrentTrackFadedOut;
    public UnityEvent OnNextTrackTriggered;

    public Track CurrentTrack => _currentTrack;
    private Track _currentTrack = null;
    private sbyte _currentIndex = 0;
    public bool HasFinished { get; private set; } = false;

    public override void StartSequence()
    {
        HasFinished = false;
        for (int i = 0; i < Tracks.Length; i++)
            Tracks[i].Hide();
        _currentIndex = -1;
        UpdateButtons();
        NextTrack();
    }

    public void PreviousTrack()
    {
        if (!enabled) return;

        if (_currentIndex == 0)
            return;

        _currentIndex--;
        ChangeTracks();
    }

    public void NextTrack()
    {
        if (!enabled) return;

        if (DeactivateLastTrack && _currentTrack != null)
        {
            OnNextTrackTriggered?.Invoke();
            FadeOutCurrentTrack(() =>
            {
                if (_currentIndex < Tracks.Length - 1)
                    NextTrack();
                else
                {
                    HasFinished = true;
                    OnSequenceFinished?.Invoke();
                }
            });
            return;
        }

        if (_currentIndex == Tracks.Length - 1)
            return;

        if (!DeactivateLastTrack) OnNextTrackTriggered?.Invoke();
        _currentIndex++;
        ChangeTracks();
    }

    private void ChangeTracks()
    {
        _currentTrack = Tracks[_currentIndex];
        _currentTrack.gameObject.SetActive(true);
        if (_currentIndex < Tracks.Length - 1)
            _currentTrack.FadeIn(FadeInTime, () =>
            {
                OnCurrentTrackFadedIn?.Invoke();
            });
        else
            _currentTrack.FadeIn(FadeInTime, () =>
            {
                OnCurrentTrackFadedIn?.Invoke();
                if (!DeactivateLastTrack)
                {
                    OnSequenceFinished?.Invoke();
                    HasFinished = true;
                }
            }
            );
    }

    private void FadeOutCurrentTrack(Action callback)
    {
        _currentTrack.FadeOut(FadeOutTime, () =>
        {
            OnCurrentTrackFadedOut?.Invoke();
            _currentTrack.gameObject.SetActive(false);
            UpdateButtons();
            _currentTrack = null;
            callback?.Invoke();
        });
    }

    private void UpdateButtons()
    {
        if (_previousButton == null || _nextButton == null)
            return;

        if (_currentIndex <= 0)
        {
            _previousButton.SetActive(false);
            _nextButton.SetActive(true);
        }
        else if (_currentIndex == Tracks.Length - 1)
        {
            _previousButton.SetActive(true);
            _nextButton.SetActive(false);
        }
        else
        {
            _previousButton.SetActive(true);
            _nextButton.SetActive(true);
        }
    }
}
