using UnityEngine;
namespace Submarine.Test
{
    public class EnemyTest : MonoBehaviour, IEnemyEffect
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _slowdownMultiplier = 0.5f;
        [SerializeField] private int _damagePoints = 5;

        private Vector3 _attackPosition;        

        public float SlowdownMultiplier => _slowdownMultiplier;
        private bool _isAttachedToPlayer = false;
        private Vector2 _position;        

        private void Start()
        {
        }

        private void OnDisable()
        {
            CancelInvoke();
            GameManager.Instance.Player.RemoveAttachedEnemy(this);
        }
        private void Update()
        {
            if (_isAttachedToPlayer)
            {
                _position = _playerController.transform.position + _attackPosition;
                transform.position = _position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _attackPosition = _playerController.GetAttackPosition();
                _isAttachedToPlayer = true;
                _playerController.AddAttachedEnemy(this);
                InvokeRepeating("DamageCharacter", 2, 2);
            }
        }

        private void DamageCharacter()
        {
            _playerController.Damage(_damagePoints);
        }
    }
}