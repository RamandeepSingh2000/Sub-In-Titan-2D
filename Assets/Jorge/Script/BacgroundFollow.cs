using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float minYLimit = -5f;
    [SerializeField] private float maxYLimit = 5f;

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);

            // limiting the scroll capacity...
            float verticalOffset = Mathf.Clamp(playerTransform.position.y, minYLimit, maxYLimit);
            transform.position = new Vector3(transform.position.x, verticalOffset, transform.position.z);
        }
    }
}

