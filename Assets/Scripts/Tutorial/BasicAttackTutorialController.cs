using System.Collections;
using UnityEngine;

public class BasicAttackTutorialController : TutorialController
{
    [SerializeField] private Biter[] _enemies;
    private int _aliveEnemiesCount;

    private void SubscribeToEvents()
    {
        for (int i = 0; i < _enemies.Length; i++)
            _enemies[i].OnDead += OnEnemyDied;
        _aliveEnemiesCount = _enemies.Length;
    }    

    private void OnEnemyDied()
    {
        _aliveEnemiesCount--;
        if (_aliveEnemiesCount == 0)
            FinishTutorial();
    }

    private IEnumerator ShowInstructions()
    {
        while(!textController.HasFinished)
        {
            yield return new WaitForSeconds(3);
            textController.NextTrack();
        }
    }


    public override void SetUp()
    {
        base.SetUp();
        SubscribeToEvents();
        playerController.AttackController.EnableModule();
        playerController.AttackController.WeaponsController.UnlimitedAmmo = true;
        playerController.AttackController.EnableBasicAttack();
        playerController.AttackController.DisableAOEAttack();
        StartCoroutine(ShowInstructions());
    }

    public override void FinishTutorial()
    {
        base.FinishTutorial();
        playerController.AttackController.DisableModule();
        playerController.AttackController.WeaponsController.UnlimitedAmmo = false;
    }

    public override void Disable()
    {
        base.Disable();
    }
}
