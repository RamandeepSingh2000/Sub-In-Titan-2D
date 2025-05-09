using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Biter : MonoBehaviour, IEnemyEffect, IEnemy
{
    [SerializeField] Animator animator;
    [SerializeField] float detectionRadius;
    [SerializeField] float speed;
    [SerializeField] float snapDistance;
    [SerializeField] float snapTime;
    [SerializeField] float aggroLoseDistance;
    [SerializeField] NavMeshAgent agent;
    Transform player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float biterWidth;
    [SerializeField] float slowDownMultiplier;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] GameObject GFX;
    [SerializeField] Collider2D collider;
    [SerializeField] float deathTime;
    [SerializeField] float attackTime;
    [SerializeField] int damage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioConfigSO bitingAudio;
    [SerializeField] AudioConfigSO deathAudio;

    public UnityAction OnDead;
    ISubmarine playerSub;
    EnemyState enemyState = EnemyState.Idle;
    Vector3 relativeAttackPosition;
    int health = 100;
    float timeLeft;
    public float SlowdownMultiplier => slowDownMultiplier;
    private bool _isAttached = false;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.stoppingDistance = 0;
        agent.enabled = false;
    }
    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        playerSub = player.GetComponent<ISubmarine>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyState == EnemyState.Attack || enemyState == EnemyState.Snap || enemyState == EnemyState.Dead)
        {
            return;
        }

        if (enemyState == EnemyState.Chase)
        {
            agent.SetDestination(player.position + relativeAttackPosition);
            spriteRenderer.flipX = (player.position + relativeAttackPosition).x - biterWidth > transform.position.x;

            Vector2 target = player.position + relativeAttackPosition;
            Vector2 current = transform.position;
            if ((target - current).magnitude <= snapDistance)
            {
                agent.enabled = false;
                enemyState = EnemyState.Snap;
                StartCoroutine(SnapCoroutine());
            }

            if ((target - current).magnitude >= aggroLoseDistance)
            {
                agent.enabled = false;
                enemyState = EnemyState.Idle;
                animator.SetBool("swim", false);
            }

            return;
        }

        if (Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer))
        {
            agent.enabled = true;
            enemyState = EnemyState.Chase;
            relativeAttackPosition = playerSub.GetRelativeAttackPosition();
            //change animation to chase
            animator.SetBool("swim", true);
        }
    }

    void DamagePlayer()
    {
        playerSub.Damage(damage);
    }
    void HandleDeath()
    {
        enemyState = EnemyState.Dead;
        StopAllCoroutines();        
        if (_isAttached)
            playerSub.RemoveAttachedEnemy(this);
        GFX.SetActive(false);
        collider.enabled = false;
        deathEffect.Play();
        audioSource.Stop();
        AudioConfigSO.SetData(deathAudio, audioSource);
        audioSource.Play();
        CancelInvoke();
        Destroy(gameObject, deathTime);
        OnDead?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (player == null)
        {
            return;
        }
        Gizmos.DrawSphere(player.position + relativeAttackPosition, 0.1f);
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            HandleDeath();
        }
        else StartCoroutine(PlayDamageAnimation());
    }

    private IEnumerator PlayDamageAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator SnapCoroutine()
    {
        spriteRenderer.flipX = (player.position + relativeAttackPosition).x - biterWidth > transform.position.x;
        timeLeft = snapTime;
        Vector2 startPosition = transform.position;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            float t = 1 - timeLeft / snapTime;
            Vector2 target = player.position + relativeAttackPosition;
            transform.position = Vector2.Lerp(startPosition, target, t);
            yield return null;
        }

        if (enemyState != EnemyState.Dead)
        {
            transform.parent = player;
            transform.position = player.position + relativeAttackPosition;
            enemyState = EnemyState.Attack;
            if (!_isAttached)
            {
                playerSub.AddAttachedEnemy(this);                
                _isAttached = true;
            }
            //change animation to attack
            animator.SetBool("attack", true);
            AudioConfigSO.SetData(bitingAudio, audioSource);
            audioSource.Play();
            InvokeRepeating(nameof(DamagePlayer), attackTime, attackTime);
        }
    }

    enum EnemyState
    {
        Idle,
        Chase,
        Snap,
        Attack,
        Dead
    }
}
