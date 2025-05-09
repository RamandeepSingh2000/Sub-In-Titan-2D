using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class HealthModule : BaseModule
    {
        [SerializeField] private int _maxHealthPoints = 200;
        public int HealthPoints { get; private set; }
        private byte _enemiesAttachedCount = 0;


        public UnityAction OnEnemyAttached;
        public UnityAction OnEnemyDetached;

        public UnityAction OnDamageReceived;
        public UnityAction OnHealthRestored;
        public UnityAction<int> OnHealthChanged;
        public UnityAction OnDie;

        private void Awake()
        {
            HealthPoints = _maxHealthPoints;
        }

        public void Restore(int points)
        {
            HealthPoints = points;
            OnHealthChanged?.Invoke(points);
            OnHealthRestored?.Invoke();
        }

        public void Damage(int points)
        {
            if (HealthPoints > 0)
            {
                HealthPoints -= points;
                OnHealthChanged?.Invoke(HealthPoints);
                OnDamageReceived?.Invoke();

                if (HealthPoints <= 0)
                    Die();
            }
        }
        public void SetHealth(int health)
        {
            HealthPoints = health;
            OnHealthChanged?.Invoke(HealthPoints);
        }

        /// <summary>
        /// Plays the dead animation and disables the controls
        /// </summary>
        public void Die()
        {
            OnDie?.Invoke();
        }

        public int GetMaxHealth() => _maxHealthPoints;
        public bool HasMaxHealth() => (HealthPoints / GetMaxHealth()) == 1;

        public void AddAttachedEnemy()
        {
            _enemiesAttachedCount++;            
            OnEnemyAttached?.Invoke();
        }

        public void RemoveAttachedEnemy()
        {
            _enemiesAttachedCount--;            
            if (_enemiesAttachedCount < 0)
                _enemiesAttachedCount = 0;

            OnEnemyDetached?.Invoke();
        }

        public bool HasEnemiesAttached() => _enemiesAttachedCount > 0;
    }
}