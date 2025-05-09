using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovementTutorialController : TutorialController
{
    [SerializeField] private Light2D _globalLight;

    private bool _hasShownMovementInstruction = false;
    private bool _hasShownDashInstruction = false;


    private void Update()
    {
        if (!_hasShownMovementInstruction)
        {
            if (playerController.MovementController.GetMovementDirection().magnitude != 0)
            {
                Invoke(nameof(ShowNextInstruction), 2);
                _hasShownMovementInstruction = true;
            }
            return;
        }

        if (!_hasShownDashInstruction)
        {
            if (playerController.MovementController.IsDashing())
            {
                Invoke(nameof(ShowNextInstruction), 2);
                _hasShownDashInstruction = true;
            }
        }
    }

    public override void SetUp()
    {
        base.SetUp();
        playerController.MovementController.EnableModule();
        playerController.AnimationsController.EnableModule();
        playerController.AnimationsController.HideLights();
        Cursor.visible = false;
        _globalLight.enabled = false;
    }

    public override void FinishTutorial()
    {
        base.FinishTutorial();
        playerController.MovementController.FullyReloadDash();
        playerController.MovementController.DisableModule();
    }

    public override void Disable()
    {
        base.Disable();
        Cursor.visible = true;
        _globalLight.enabled = true;
        playerController.AnimationsController.ShowLights();
    }

    private void ShowNextInstruction()
    {
        if (!textController.HasFinished)
            textController.NextTrack();
    }
}
