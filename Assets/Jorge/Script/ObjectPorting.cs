using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPorting : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float triggerYValue = 10f;
    [SerializeField] private float respawn = 10f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 2);
    }

    private void FixedUpdate()
    {
        // Check if object's Y position is greater than or equal to triggerYValue
        if (transform.position.y >= triggerYValue)
        {
            // Change sprite to a random one from the sprites array
            if (sprites.Length > 0)
            {
                int randomIndex = Random.Range(0, sprites.Length);
                spriteRenderer.sprite = sprites[randomIndex];
            }
            rb.velocity = new Vector2(0,2);
            // Move the object back to the bottom of the screen
            transform.position = new Vector3(transform.position.x, -respawn, transform.position.z);
        }
    }
}
