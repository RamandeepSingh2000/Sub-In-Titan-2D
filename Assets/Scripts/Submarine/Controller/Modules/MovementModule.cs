using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class MovementModule : BaseModule
    {
        [Header("Physics")]
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Movement Specs")]
        [SerializeField] private float _normalSpeed = 7f;
        [SerializeField] private float _linearDrag = 1.5f;

        [Header("Dash")]
        [SerializeField] private float _dashSpeed = 12f;
        [SerializeField] private float _dashDuration = 2.0f;
        [SerializeField] private float _dashRecoveryRate = 0.5f;

        private float _dashRemainingTime = 0;
        private bool _isDashing = false;

        private Vector2 _movementInputs;
        private float _speed = 0;
        private float _speedMultiplier = 1f;
        private Vector2 _movement;

        public UnityAction OnCollision;

        private void Awake()
        {
            _speed = _normalSpeed;
            _rigidbody.drag = _linearDrag;
            _dashRemainingTime = _dashDuration;
        }

        private void FixedUpdate()
        {
            if (!IsEnabled)
                return;

            Move();

            UpdateDashUsage();
        }

        public override void EnableModule()
        {
            base.EnableModule();
        }

        public override void DisableModule()
        {
            base.DisableModule();
        }

        public void SetUserMovementInput(Vector2 movementInputs)
        {
            _movementInputs = movementInputs;
        }

        #region DASH

        public void FullyReloadDash() => _dashRemainingTime = _dashDuration;

        public bool IsDashing() => _speed == _dashSpeed;

        public void StartDashing() => _isDashing = true;

        public void StopDashing() => _isDashing = false;

        private void UpdateDashUsage()
        {
            if (_isDashing && _movement.magnitude != 0)
            {
                if (_dashRemainingTime > 0)
                    _dashRemainingTime -= Time.fixedDeltaTime;
                else _dashRemainingTime = 0;
            }
            else if (!_isDashing)
            {
                if (_dashRemainingTime < _dashDuration)
                    _dashRemainingTime += Time.fixedDeltaTime * _dashRecoveryRate;
                else _dashRemainingTime = _dashDuration;
            }
        }

        public float GetRemainingDashPercentage()
        {
            return _dashRemainingTime / _dashDuration;
        }

        private bool CanDash() => _dashRemainingTime > 0;

        #endregion

        public Vector2 GetMovementDirection() => _movementInputs;
        public Vector2 GetVelocity() => _rigidbody.velocity;

        private void Move()
        {
            if (_isDashing && CanDash())
                _speed = _dashSpeed;
            else _speed = _normalSpeed;

            _movement = _movementInputs * _speed;
            Push(_movement);
        }

        public void Push(Vector2 forceVector)
        {
            _rigidbody.AddForce(forceVector, ForceMode2D.Force);
        }

        public void DecreaseSpeedBy(float multiplier)
        {
            _speedMultiplier -= multiplier;
            if (_speedMultiplier < 0)
                _speedMultiplier = 0;
        }

        public void IncreaseSpeedBy(float multiplier)
        {
            _speedMultiplier += multiplier;
            if (_speedMultiplier > 1)
                _speedMultiplier = 1;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke();
        }
    }
}