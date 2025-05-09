using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementTest : MonoBehaviour
{
    InputsJorgeTest controls;
    Rigidbody2D rb;

    [Header("Player parameters")]
    [SerializeField] float _speed = 5f;
    [SerializeField] float _propelForce = 10f;
    [SerializeField] float _propelDuration = 1f;
    CharacterController _controller;
    float propelTimer = 0f;

    private void Awake()
    {
        controls = new InputsJorgeTest();
        rb = GetComponent<Rigidbody2D>();

        _controller = GetComponent<CharacterController>();

        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += _ => Move(Vector2.zero);
        controls.Player.Fire.started += _ => Propel();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        if (controls.Player.enabled)
        {
            Move(controls.Player.Move.ReadValue<Vector2>());
        }
        else
        {
            Move(Vector2.zero);
        }


        if (propelTimer > 0f)
        {
            rb.AddForce(rb.velocity.normalized * _propelForce, ForceMode2D.Impulse);
            propelTimer -= Time.fixedDeltaTime;
        }
    }


    void Move(Vector2 direction)
    {
        //Debug.Log("Direction: " + direction);
        rb.velocity = new Vector2(direction.x * _speed, /*rb.velocity*/direction.y * _speed);
    }

    void Propel()
    {
        // Start the timer for propulsion
        propelTimer = _propelDuration;
    }
}