public class RepairTutorialController : TutorialController
{
    private bool _hasShownOpenMenuInstruction = false;
    private bool _hasShownClickRepairInstruction = false;

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

        playerController.ActionMenuController.RepairController.OnProcessStarted += () =>
        {
            if (!_hasShownClickRepairInstruction)
            {
                ShowNextInstruction();
                _hasShownClickRepairInstruction = true;
            }
        };

        playerController.ActionMenuController.RepairController.OnProcessFinished += FinishTutorial;
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
        playerController.AnimationsController.EnableModule();
        playerController.HealthController.EnableModule();
        playerController.HealthController.Damage(180);
        playerController.ActionMenuController.EnableModule();
    }

    public override void FinishTutorial()
    {
        base.FinishTutorial();
        playerController.ActionMenuController.DisableModule();
    }

    public override void Disable()
    {
        base.Disable();
        playerController.ActionMenuController.Close();
    }
}