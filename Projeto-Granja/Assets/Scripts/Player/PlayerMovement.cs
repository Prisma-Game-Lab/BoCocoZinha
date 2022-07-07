using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movX;
    private float movY;
    private Vector2 movVector;
    private int moveSpeed;
    private Animator animator;

    [Header("Atributos Dash")]
    public float dashCooldownTimer;
    private float currentDashCooldownTimer;
    public float dashDuration;
    private float currentDashDuration;
    public int dashSpeed;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        moveSpeed = GetComponent<PlayerStats>().speed;

        currentDashCooldownTimer = 0;
        isDashing = false;
    }

    void OnMove(InputValue mov)
    {
        movVector = mov.Get<Vector2>();
        movX = movVector.x;
        movY = movVector.y;

        if (movX < 0)
        {
            animator.SetTrigger("Side");
            this.gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        } 
        else if (movX > 0)
        {
            this.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            animator.SetTrigger("Side");
        }

        if(movY < 0)
        {
            animator.SetTrigger("Down");
        }
        else if(movY > 0)
        {
            animator.SetTrigger("Up");
        }

        if(movX == 0 && movY == 0)
        {
            animator.SetTrigger("Idle");
        }
    }

    void OnDash()
    {
        
        if (currentDashCooldownTimer <= 0 && !isDashing)
        {
            Debug.Log(movVector.x);
            currentDashDuration = dashDuration;
            currentDashCooldownTimer = dashCooldownTimer;
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {

        if (currentDashCooldownTimer > 0)
        {
            currentDashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            if (currentDashDuration > 0)
            {
                rb.MovePosition(rb.position + movVector * dashSpeed * Time.fixedDeltaTime);
                currentDashDuration -= Time.deltaTime;
            } else
            {
                isDashing = false;
            }

        } else
        {
            rb.MovePosition(rb.position + movVector * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
