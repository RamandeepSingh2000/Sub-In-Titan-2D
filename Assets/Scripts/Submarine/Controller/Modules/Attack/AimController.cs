using UnityEngine;

namespace Submarine
{
    public class AimController : BaseModule
    {
        private Vector2 _aimDirection = Vector2.zero;

        [SerializeField] private Transform _cannonTransform;

        public Vector2 GetAimDirection() => _aimDirection;

        public void UpdateAimDirection(Vector2 direction)
        {
            if (!IsEnabled)
                return;

            CalculateAimDirection(direction);
        }

        private void CalculateAimDirection(Vector2 direction)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(direction.x, direction.y, 10));
            _aimDirection = mousePosition - transform.position;

            float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;

            _cannonTransform.eulerAngles = Vector3.forward * angle;
        }
    }
}