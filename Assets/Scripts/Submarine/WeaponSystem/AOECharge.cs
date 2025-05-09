using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOECharge : MonoBehaviour
{
    [SerializeField] float chargeRadius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int damage;
    [SerializeField] float chargeTime;
    [SerializeField] float damageTime;
    [SerializeField] Transform effect_FirstCircle;
    [SerializeField] Transform effect_SecondCircle;
    float timeAccumulator;
    AOEState state = AOEState.None;
    public bool IsAOEReady => state == AOEState.Ready;

    public void Update()
    {
        if (state == AOEState.Charging)
        {
            timeAccumulator += Time.deltaTime;
            if(timeAccumulator <= chargeTime)
            {
                effect_FirstCircle.localScale = Vector3.one * Mathf.Lerp(0, chargeRadius, timeAccumulator / chargeTime);
                return;
            }
            effect_FirstCircle.localScale = Vector3.one * chargeRadius;
            state = AOEState.Ready;
        }
        if(state == AOEState.Using)
        {
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator <= damageTime)
            {
                effect_SecondCircle.localScale = Vector3.one * Mathf.Lerp(0, chargeRadius, timeAccumulator / damageTime);
                return;
            }
            effect_SecondCircle.localScale = Vector3.one * chargeRadius;
            DamageEnemies();
            state = AOEState.None;
            effect_FirstCircle.localScale = Vector3.zero;
            effect_SecondCircle.localScale = Vector3.zero;
        }
    }
    public void UseCharge()
    {
        if(state != AOEState.Ready)
        {
            return;
        }

        state = AOEState.Using;
        timeAccumulator = 0;        
    }
    void DamageEnemies()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, chargeRadius, enemyLayer);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<IEnemy>().Damage(damage);
        }
    }
    public void StartCharging()
    {
        if(state != AOEState.None)
        {
            return;
        }

        state = AOEState.Charging;
        timeAccumulator = 0;
    }
    public void CancelCharge()
    {
        if(state != AOEState.Charging)
        {
            return;
        }
        effect_FirstCircle.localScale = Vector3.zero;
        state = AOEState.None;
    }

    enum AOEState{
        None,
        Charging,
        Ready,
        Using,
    }
}
