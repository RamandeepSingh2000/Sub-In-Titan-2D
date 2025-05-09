using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IEnemy
{
    public void Damage(int damage)
    {
        Debug.Log("Damaged");
    }
}
