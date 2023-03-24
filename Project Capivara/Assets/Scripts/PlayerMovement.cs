using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float speed;
    Vector2 motionVector;
    Animator animator;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        motionVector = new Vector2
            (Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("speed", motionVector.sqrMagnitude);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2D.velocity = motionVector * speed * Time.fixedDeltaTime;
    }
}
