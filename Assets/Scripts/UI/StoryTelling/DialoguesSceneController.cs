using UnityEngine;

public class DialoguesSceneController : MonoBehaviour
{
    [SerializeField] private ControlledTracksActivationController[] _tracksControllers;
    private ControlledTracksActivationController _currentTrackController;
    private byte _currentIndex = 0;

    private void Start()
    {
        ChangeTrack(_tracksControllers[_currentIndex]);
    }

    public void Next()
    {
        if (_currentTrackController == null)
            return;

        //if (_currentTrackController.CurrentTrack == null)
        //    return;

        if (_currentTrackController.CurrentTrack != null && _currentTrackController.CurrentTrack.IsPlayingAnimation)
            return;

        if (!_currentTrackController.HasFinished)
            _currentTrackController.NextTrack();
        else
        {
            if (_currentIndex < _tracksControllers.Length - 1)
            {
                _currentIndex++;
                _currentTrackController.gameObject.SetActive(false);
                ChangeTrack(_tracksControllers[_currentIndex]);
            }
        }
    }

    private void ChangeTrack(ControlledTracksActivationController trackController)
    {
        trackController.gameObject.SetActive(true);
        _currentTrackController = trackController;
    }
}
