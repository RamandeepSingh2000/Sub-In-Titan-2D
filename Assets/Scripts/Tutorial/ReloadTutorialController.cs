public class ReloadTutorialController : TutorialController
{
    private bool _hasShownOpenMenuInstruction = false;
    private bool _hasShownClickReloadInstruction = false;

    private void SubscribeToEvents()
    {
        playerController.ActionMenuController.OnOpen += () =>
        {
            if (!_hasShownOpenMenuInstruction)
            {
                ShowNextInstruction();
                _hasShownOpenMenuInstruction = true;
            }
        };

        playerController.ActionMenuController.ReloadController.OnProcessStarted += () =>
        {
            if (!_hasShownClickReloadInstruction)
            {
                ShowNextInstruction();
                _hasShownClickReloadInstruction = true;
            }
        };

        playerController.ActionMenuController.ReloadController.OnProcessFinished += FinishTutorial;
    }

    private void OnDisable()
    {
        playerController.AttackController.OnAOEShot -= FinishTutorial;
    }

    public void ShowNextInstruction()
    {
        if (!textController.HasFinished)
            textController.NextTrack();
    }

    public override void SetUp()
    {
        base.SetUp();
        SubscribeToEvents();
        playerController.AttackController.AimController.EnableModule();
        playerController.AttackController.ReloadCannon(0);
        playerController.ActionMenuController.EnableModule();
    }

    public override void FinishTutorial()
    {
        base.FinishTutorial();
    }

    public override void Disable()
    {
        base.Disable();
        playerController.ActionMenuController.Close();
        playerController.ActionMenuController.DisableModule();
        playerController.DisableActionMenuActionMap();
        playerController.EnablePlayerActionMap();
    }
}