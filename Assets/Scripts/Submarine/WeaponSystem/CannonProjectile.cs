using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float explosionRadius;
    [SerializeField] float range;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask physicsLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float destroyTime;
    [SerializeField] GameObject GFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioConfigSO explosionAudio;
    bool hasExploded;
    Vector3 launchPoint;
    bool shot;
    SubWeaponsHandler weaponHandler;
    public void InjectDependency(SubWeaponsHandler weaponHandler)
    {
        this.weaponHandler = weaponHandler;
    }

    private void OnEnable()
    {
        ResetProjectile();
    }
    public void ResetProjectile()
    {
        hasExploded = false;
        explosionEffect.SetActive(false);
        rb.isKinematic = false;
        GFX.SetActive(true);
        shot = false;
    }
    public void Shoot(Vector2 direction)
    {
        launchPoint = transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * direction);
        rb.velocity = direction * speed;
        rb.isKinematic = false;
        shot = true;
        explosionEffect.transform.localScale = Vector3.one * explosionRadius;
    }
    private void Update()
    {
        if (!shot)
        {
            return;
        }
        if (hasExploded)
        {
            return;
        }
        if((transform.position - launchPoint).sqrMagnitude >= range * range)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded)
        {
            return;
        }

        if (((1 << other.gameObject.layer) & physicsLayer) > 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        explosionEffect.SetActive(true);
        AudioConfigSO.SetData(explosionAudio, audioSource);
        audioSource.Play();
        rb.isKinematic = true;
        GFX.SetActive(false);
        var enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<IEnemy>().Damage(damage);
        }

        Invoke(nameof(ReturnToPool), destroyTime);
    }
    void ReturnToPool()
    {
        weaponHandler.ReturnCannonProjectile(this);
    }
}
