using Submarine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class FishEndingSequence : MonoBehaviour
{
    [SerializeField] Light2D leftEyeLight;
    [SerializeField] Light2D rightEyeLight;
    [SerializeField] Light2D fishLight;
    [SerializeField] Light2D shipLight;
    [SerializeField] Transform fish;
    [SerializeField] float fishMaxSize;
    [SerializeField] float eyeMaxRadius;
    [SerializeField] float fishMaxRadius;
    [Range(0, 1f)]
    [SerializeField] float lightStartRatio;
    [Range(0, 1f)]
    [SerializeField] float eyeLightRatio;
    [Range(0, 1f)]
    [SerializeField] float lightEndRatio;
    [SerializeField] float maxShipLightSize;
    [SerializeField] float totalFishTime;
    [SerializeField] float shipTime;
    [Range(0, 1f)]
    [SerializeField] float shipStartRatio;
    [SerializeField] MovementModule movementModule;
    [SerializeField] Transform shipImage;
    bool startedShip;

    public UnityEvent OnSequenceEnd;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FishEndingCoroutine());
            if (movementModule == null) { Debug.Log("Movement Module not assigned"); }
            else { movementModule.DisableModule(); }
            if (shipImage == null) { Debug.Log("Ship Image not assigned"); }
            else { shipImage.position = collision.gameObject.transform.position; }
        }
    }

    IEnumerator FishEndingCoroutine()
    {
        leftEyeLight.pointLightOuterRadius = 0;
        rightEyeLight.pointLightOuterRadius = 0;
        fishLight.pointLightOuterRadius = 0;
        shipLight.pointLightOuterRadius = maxShipLightSize;
        float timeAccumulator = 0;
        while (fish.localScale.x < fishMaxSize)
        {
            timeAccumulator += Time.deltaTime;
            float t = timeAccumulator / totalFishTime;
            fish.localScale = Mathf.Lerp(0, fishMaxSize, t) * Vector3.one;

            if (t > lightStartRatio && t < eyeLightRatio)
            {
                float tl = Map01(lightStartRatio, eyeLightRatio, t);
                leftEyeLight.pointLightOuterRadius = Mathf.Lerp(0, 1, tl) * eyeMaxRadius;
                rightEyeLight.pointLightOuterRadius = Mathf.Lerp(0, 1, tl) * eyeMaxRadius;
            }
            if (t > eyeLightRatio && t < lightEndRatio)
            {
                float tl = Map01(eyeLightRatio, lightEndRatio, t);
                fishLight.pointLightOuterRadius = Mathf.Lerp(0, 1, tl) * fishMaxRadius;
            }

            if (!startedShip && t >= shipStartRatio)
            {
                startedShip = true;
                StartCoroutine(ShipRoutine());
            }

            yield return null;
        }
        OnSequenceEnd?.Invoke();
    }

    IEnumerator ShipRoutine()
    {
        float timeAccumulator = 0;
        while (shipLight.pointLightOuterRadius > 0)
        {
            timeAccumulator += Time.deltaTime;
            float t = timeAccumulator / shipTime;
            shipLight.pointLightOuterRadius = (1 - Mathf.Lerp(0, 1, t)) * maxShipLightSize;
            yield return null;
        }
    }

    static float Map01(float min, float max, float value)
    {
        return (value - min) / (max - min);
    }
}
