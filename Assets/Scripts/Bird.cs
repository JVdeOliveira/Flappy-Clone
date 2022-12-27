using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        Playing,
        Died
    }
    private enum Animation
    {
        Flaying,
        Died
    }

    private const float MAX_Y_POSITION = 4.6f;

    private Rigidbody2D rb;
    private Animator animator;

    private float jumpForce = 16f;
    private State state;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        animator = GetComponentInChildren<Animator>();
        animator.speed = 0;

        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
                {
                    Jump();

                    rb.isKinematic = false;
                    state = State.Playing;
                    SetAnimation(Animation.Flaying);
                }
                break;

            case State.Playing:
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                if (transform.position.y > MAX_Y_POSITION)
                {
                    rb.velocity = Vector3.zero;
                }

                break;

            case State.Died:
                break;
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
    }

    private void SetAnimation(Animation animation)
    {
        switch (animation)
        {
            case Animation.Flaying: animator.speed = 1; break;

            case Animation.Died: animator.SetTrigger(animation.ToString()); break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = State.Died;
        SetAnimation(Animation.Died);
    }
}
