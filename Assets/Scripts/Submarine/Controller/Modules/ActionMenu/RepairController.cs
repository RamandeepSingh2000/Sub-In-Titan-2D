using UnityEngine;

namespace Submarine
{
    public class RepairController : SubmarineProcess
    {
        [Tooltip("Health percentage restored per session")]
        [SerializeField] private HealthModule _healthModule;

        private void OnEnable()
        {
            _healthModule.OnEnemyAttached += CancelProcess;
            _healthModule.OnEnemyDetached += UpdateProcess;
        }

        private void OnDisable()
        {
            _healthModule.OnEnemyAttached -= CancelProcess;
            _healthModule.OnEnemyDetached -= UpdateProcess;
        }

        public override void StartProcess()
        {
            float timeMultiplier = (float)_healthModule.HealthPoints / (float)_healthModule.GetMaxHealth();
            processTime = fullProcessDuration * timeMultiplier;
            base.StartProcess();
        }

        public override void CancelProcess()
        {
            if (!IsPerformingProcess)
                return;
            base.CancelProcess();
            int health = (int)(_healthModule.GetMaxHealth() * GetProgress());
            _healthModule.Restore(health);
        }

        protected override void FinishProcess()
        {
            _healthModule.Restore(_healthModule.GetMaxHealth());
            base.FinishProcess();
        }

        protected override void Update()
        {
            base.Update();
            if (IsPerformingProcess)
            {
                int health = (int)(_healthModule.GetMaxHealth() * GetProgress());
                _healthModule.Restore(health);
            }
        }

        private void UpdateProcess()
        {
            if (_healthModule.HasEnemiesAttached() && IsPerformingProcess)
                CancelProcess();
        }
    }
}