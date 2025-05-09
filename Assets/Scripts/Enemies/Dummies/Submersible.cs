using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submersible : MonoBehaviour, ISubmarine
{
    [SerializeField] float radius;
    [SerializeField] SubWeaponsHandler weaponHandler;
    public Transform Transform { get => transform; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            weaponHandler.SetCannonAimDirection(Random.insideUnitCircle.normalized);
        }
        if (weaponHandler.CanFireCannon && Input.GetKeyDown(KeyCode.N))
        {
            weaponHandler.FireCannon();
        }
        if (weaponHandler.CanUseAOE && Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(UseAOE());
        }
    }

    IEnumerator UseAOE()
    {
        weaponHandler.ChargeAOE();
        while (!weaponHandler.IsAOEReady)
        {
            yield return null;
        }
        weaponHandler.UseAOE();
    }
    public void AddAttachedEnemy(IEnemyEffect enemyEffect)
    {
        
    }

    public void Damage(int amount)
    {
        Debug.Log("Damaged");
    }

    public Vector2 GetRelativeAttackPosition()
    {
        Vector2 offset = radius * Random.insideUnitCircle.normalized;
        return new Vector3(offset.x, offset.y, 0);
    }

    public void RemoveAttachedEnemy(IEnemyEffect enemyEffect)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
