using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundShipMotors : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float pitchMultiplier = 0.2f;

    void Update()
    {

        if (rb != null && audioSource != null)
        {
            float velocityMagnitude = rb.velocity.magnitude;
            float pitch = .5f + velocityMagnitude * pitchMultiplier;
            audioSource.pitch = Mathf.Clamp(pitch, 0.1f, 3f);
        }
    }
}
