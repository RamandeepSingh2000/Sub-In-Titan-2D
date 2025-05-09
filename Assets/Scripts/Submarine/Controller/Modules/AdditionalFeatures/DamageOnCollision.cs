using UnityEngine;

namespace Submarine.Additional
{
    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField] private int _healthPoints;

        [Header("Modules")]
        [SerializeField] private MovementModule _movementModule;
        [SerializeField] private HealthModule _healthModule;

        private void OnEnable()
        {
            _movementModule.OnCollision += ApplyDamage;
        }

        private void OnDisable()
        {
            _movementModule.OnCollision -= ApplyDamage;
        }

        private void ApplyDamage()
        {
            if (_movementModule.IsDashing())
                _healthModule.Damage(_healthPoints);
        }
    }
}