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
    private PlayerAttack playerAttack;

    [Header("Atributos Dash")]
    public float dashCooldownTimer;
    public float dashingSpeed;
    public float dashingDuration;
    public bool isDashing;
    private bool canDash;
    private Vector2 dashingDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();

        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        moveSpeed = GetComponent<PlayerStats>().speed;

        isDashing = false;
        canDash = true;
    }

    void OnMove(InputValue mov)
    {
        if(!isDashing)
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
            
            if (movX == 0 && movY == 0)
            {
                animator.ResetTrigger("Up");
                animator.ResetTrigger("Side");
                animator.ResetTrigger("Down");
                animator.SetTrigger("Idle");
            }
        }
    }

    void OnDash()
    {  
        if (!isDashing && canDash)
        {
            //nimator.SetTrigger("Dash");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashingDirection = new Vector2(movX, movY);
        if(dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0);
        }

        rb.velocity = dashingDirection.normalized * dashingSpeed;

        yield return new WaitForSeconds(dashingDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldownTimer);

        canDash = true;
    }

    private void FixedUpdate()
    {
        if(!isDashing && !playerAttack.isAttacking)
        {
            rb.MovePosition(rb.position + movVector * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
