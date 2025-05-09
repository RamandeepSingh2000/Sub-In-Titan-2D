using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTelegraph : MonoBehaviour
{
    [SerializeField] private Color hurtColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    [SerializeField] private bool iHurt = false;
    private float flickingTime = 15;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        if (iHurt)
        {
            spriteRenderer.color = spriteRenderer.color == initialColor ? hurtColor : initialColor;
            Debug.Log(spriteRenderer.color);
            flickingTime--;
            if (flickingTime < 0) { 
                iHurt = false; flickingTime = 15;
                spriteRenderer.color = initialColor;
            }
        }

        
    }
    public void ShowImHurt() => iHurt = true;
}
