using Submarine;
using UnityEngine;

public abstract class TutorialController : MonoBehaviour
{
    [SerializeField] protected TutorialSteps step;
    [SerializeField] protected TutorialManager tutorialManager;

    [Space]
    [SerializeField] protected PlayerController playerController;
    [SerializeField] private Transform _positionTransform;
    [SerializeField] protected ControlledTracksActivationController textController;

    public virtual void SetUp()
    {
        playerController.DisableModules();
        playerController.Transform.position = _positionTransform.position;
        textController.gameObject.SetActive(true);
        textController.StartSequence();
    }

    public virtual void FinishTutorial()
    {
        tutorialManager.FinishStep((int)step);
    }

    public virtual void Disable()
    {
        textController.gameObject.SetActive(false);
    }
}
