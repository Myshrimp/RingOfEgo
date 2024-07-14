using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveComponent : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    new Rigidbody2D rigidbody;

    public float moveSpeed = 10f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    private void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnableGameplayInput();
    }

    private void Move(Vector2 moveInput)
    {
        Vector2 moveAmount = moveInput * moveSpeed;
        rigidbody.velocity = moveAmount;
    }
    private void StopMove()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
