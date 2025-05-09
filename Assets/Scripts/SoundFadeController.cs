using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFadeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _maxVolume = 1;
    [SerializeField] private bool _playOnStart = true;

    private void Start()
    {
        if (_playOnStart)
        {
            _audioSource.volume = 0;
            FadeIn(_fadeInTime);
        }
    }

    public void FadeIn(float duration = 1)
    {
        DOTween.To(() => _audioSource.volume, x => _audioSource.volume = x, _maxVolume, duration);
    }

    public void FadeOut(float duration = 1)
    {
        DOTween.To(() => _audioSource.volume, x => _audioSource.volume = x, 0, duration);
    }
}
