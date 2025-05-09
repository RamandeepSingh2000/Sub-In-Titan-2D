using UnityEngine;

public class DialogueTriggerArea : TriggerArea
{
    [SerializeField] private TracksActivationController _dialogue;
    [SerializeField] private bool _deactivatePlayerControl = false;
    private bool _alreadyPlayed = false;

    private void OnEnable()
    {
        _dialogue.OnSequenceFinished.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        if (_deactivatePlayerControl)
            GameManager.Instance.Player.EnablePlayerActionMap();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_alreadyPlayed)
            return;
        
        _alreadyPlayed = true;
        if (_deactivatePlayerControl)
        {
            GameManager.Instance.Player.DisablePlayerActionMap();
            GameManager.Instance.Player.DisableActionMenuActionMap();
        }
        base.OnTriggerEnter2D(collision);
        _dialogue.gameObject.SetActive(true);
        _dialogue.StartSequence();
    }

    private void Deactivate()
    {
        _dialogue.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
