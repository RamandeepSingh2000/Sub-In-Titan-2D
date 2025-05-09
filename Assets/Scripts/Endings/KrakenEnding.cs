using Submarine;
using UnityEngine;

public class KrakenEnding : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _krakenTail;

    public void AttachSubmersibleToKrakenTail()
    {
        SaveManager.levelChange = true;
        _playerController.transform.SetParent(_krakenTail);
        _playerController.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void SetUpPlayer()
    {
        _playerController.DisableActionMenuActionMap();
        _playerController.DisablePlayerActionMap();
        _playerController.GetComponent<Collider2D>().enabled = false;
        _playerController.SoundEffectsController.MuteAllSources();
    }
}
