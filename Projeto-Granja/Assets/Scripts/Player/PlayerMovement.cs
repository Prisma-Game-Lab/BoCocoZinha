using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movX;
    [SerializeField] private float movY;
    private Vector2 movVector;
    private int moveSpeed;
    private Animator animator;
    private PlayerAttack playerAttack;

    [Header("Animação")] 
    [Range(0,1)] public float movementDeadzone = 0.1f;
    
    [Header("Atributos Dash")]
    public float dashCooldownTimer;
    public float dashingSpeed;
    public float dashingDuration;
    public bool isDashing;
    private bool canDash;
    private Vector2 dashingDirection;
    
    //Animator hashes
    private int animMoving = Animator.StringToHash("Moving");
    private int animDashing = Animator.StringToHash("Dashing");
    private int animAttack = Animator.StringToHash("Attack");
    private int animHorizontal = Animator.StringToHash("Horizontal");
    private int animVertical = Animator.StringToHash("Vertical");

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
        if (isDashing) return;
        
        movVector = mov.Get<Vector2>();
        
        if (movVector.magnitude > movementDeadzone && !playerAttack.isCharging)
        {
            // cache movement input in separate axis
            movX = movVector.x;
            movY = movVector.y;
            
            animator.SetBool(animMoving, true);
            animator.SetFloat(animHorizontal, movX);
            animator.SetFloat(animVertical, movY);

            playerAttack.UpdateAttackPoint(movVector);
        }
        else
        {
            // clear vector to prevent controller drift
            movVector = Vector2.zero; 
            animator.SetBool(animMoving,false);
        }
    }

    void OnDash()
    {  
        if (!isDashing && canDash && !playerAttack.isAttacking)
        {
            StartCoroutine(Dash());
        }
    }

    public void playSteps()
    {
        string soundType1, soundType2;
        soundType1 = "step_1";
        soundType2 = "step_2";

        int index = Random.Range(0, 2);
        if (index == 0)
            AudioManager.instance.Play(soundType1);
        else
        {
            AudioManager.instance.Play(soundType2);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashingDirection = new Vector2(movX, movY);

        AudioManager.instance.Play("player_dash");
        animator.SetBool(animDashing,true);
        
        if(dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0);
        }

        rb.velocity = dashingDirection.normalized * dashingSpeed;

        yield return new WaitForSeconds(dashingDuration);

        isDashing = false;
        rb.velocity = Vector2.zero;
        animator.SetBool(animDashing,false);
        
        yield return new WaitForSeconds(dashCooldownTimer);

        canDash = true;
    }

    private void FixedUpdate()
    {
        if (!playerAttack) return;
        if (isDashing || playerAttack.isAttacking || playerAttack.isCharging) return;
        
        rb.MovePosition(rb.position + movVector * moveSpeed * Time.fixedDeltaTime);
    }
}
