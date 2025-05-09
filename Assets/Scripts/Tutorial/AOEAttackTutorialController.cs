public class AOEAttackTutorialController : TutorialController
{
    private bool _checkAOECharge = false;
    private void SubscribeToEvents()
    {
        playerController.AttackController.OnAOECharge += () => _checkAOECharge = true;
        playerController.AttackController.OnAOECanceled += () => _checkAOECharge = false;
        playerController.AttackController.OnAOEShot += ShowLastInstruction;
    }

    private void Update()
    {
        if(_checkAOECharge && playerController.AttackController.WeaponsController.IsAOEReady)
        {
            ShowNextInstruction();
            _checkAOECharge = false;
        }
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

    private void ShowLastInstruction()
    {
        ShowNextInstruction();
        Invoke(nameof(FinishTutorial), 5);
    }

    public override void SetUp()
    {
        base.SetUp();
        SubscribeToEvents();
        playerController.AttackController.EnableModule();
        playerController.AttackController.DisableBasicAttack();
        playerController.AttackController.EnableAOEAttack();
    }

    public override void FinishTutorial()
    {
        base.FinishTutorial();
        playerController.AttackController.DisableModule();
    }

    public override void Disable()
    {
        base.Disable();
    }
}