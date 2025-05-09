using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    public bool IsPlayerVisible { get; private set; }

    private void Update()
    {
        
    }
}
