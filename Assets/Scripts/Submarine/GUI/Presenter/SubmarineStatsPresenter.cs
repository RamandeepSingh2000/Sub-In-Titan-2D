using UnityEngine;

namespace Submarine.UI
{
    public class SubmarineStatsPresenter : MonoBehaviour
    {
        [SerializeField] private SubmarineStatsView _view;
        [SerializeField] private HealthModule _healthModule;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private MovementModule _movementModule;
        private bool _updateAOECooldown = false;

        private void OnEnable()
        {
            _healthModule.OnHealthChanged += UpdateHealth;
            _attackModule.OnBasicAttackShot += UpdateAmmo;
            _attackModule.OnCannonReloaded += UpdateAmmo;
            _attackModule.OnAOEShot += () => _updateAOECooldown = true;
        }

        private void OnDisable()
        {
            _healthModule.OnHealthChanged -= UpdateHealth;
            _attackModule.OnBasicAttackShot -= UpdateAmmo;
            _attackModule.OnCannonReloaded -= UpdateAmmo;
            _attackModule.OnAOEShot -= () => _updateAOECooldown = false;
        }

        private void Start()
        {
            Invoke("UpdateAmmo", 0.5f);
            UpdateHealth(_healthModule.HealthPoints);
        }


        private void UpdateHealth(int points)
        {
            float percentage = (float)points / (float)_healthModule.GetMaxHealth();
            _view.UpdateHealth(percentage);
        }

        private void Update()
        {
            _view.UpdateDash(_movementModule.GetRemainingDashPercentage());
            if (_updateAOECooldown)
                UpdateAOECooldown();

        }

        #region ATTACK

        private void UpdateAmmo()
        {
            _view.UpdateAmmo(_attackModule.GetCannonAmmo());
        }

        private void UpdateAOECooldown()
        {
            float percentage = _attackModule.WeaponsController.GetAOECooldownPercentage();            
            _view.UpdateAOECooldown(percentage);
            if (percentage >= 1)
                _updateAOECooldown = false;
        }

        #endregion
    }
}