using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEffect : MonoBehaviour
{
    [Header("Sound Sources")]
    [SerializeField] private AudioSource audioSource;

    [SerializeField] Animator animator;

    public void PlaySFX()
    {
        animator.SetBool("isSaving", true);
        audioSource.Play();
    }
}
