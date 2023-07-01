using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public bool attacking;
    [HideInInspector] public bool isAttacking;

    private Rigidbody2D rb;

    private float moveTimer;
    private int lastDirection; //0-Up, 1-Down, 2-Left, 3-Right
    private int newDirection;
    private Vector2 movVector;

    public int movLimit;
    private int[] moved = new int[4];

    public EnemyStats stats;

    [SerializeField] private GameObject player;
    public bool knockback;
    public float followTreshold;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        knockback = false;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        lastDirection = 0;

        moved[0] = 0; //up
        moved[1] = 0; //down
        moved[2] = 0; //left
        moved[3] = 0; //right

        animator = GetComponent<Animator>();

    }

    Vector2 lastPosition;
    int dir = 1;
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (!knockback)
        {
            if (attacking && player != null)
            {
                ChasePlayer();
                animator.SetBool("Walking", true);
            }
            else if (!isAttacking)
            {
                PatrolMovement();
                animator.SetBool("Walking", true);
            }

        }
        else
            animator.SetBool("Walking", false);
        if (lastPosition.x < this.transform.position.x && dir == 1)
        {
            dir = -1;
            this.gameObject.transform.Rotate(Vector3.up, 180);
        }
        else if (lastPosition.x > this.transform.position.x && dir == -1)
        {
            dir = 1;
            this.gameObject.transform.Rotate(Vector3.up, 180);
        }




        lastPosition = this.transform.position;


    }


    void PatrolMovement()
    {
        if (moveTimer >= 1)
        {
            moveTimer = 0;
            lastDirection = newDirection;

        }
        else if (moveTimer == 0)
        {
            //Escolhe uma direcao aleatoria
            newDirection = Random.Range(0, 4);

            while ((newDirection == lastDirection) || (moved[newDirection] == movLimit))
            {
                newDirection = Random.Range(0, 4);
            }

            CalculateVector();

            moveTimer += Time.deltaTime;
            rb.MovePosition(rb.position + movVector * stats.speed * Time.fixedDeltaTime);


        }
        else
        {
            moveTimer += Time.deltaTime;
            rb.MovePosition(rb.position + movVector * stats.speed * Time.fixedDeltaTime);
        }


    }

    void CalculateVector()
    {
        switch (newDirection)
        {
            default:
                Debug.Log("Erro escolhendo direcao");
                break;

            case 0:
                movVector = new Vector2(0.0f, 1.0f);
                moved[0]++;
                moved[1]--;
                break;

            case 1:
                movVector = new Vector2(0.0f, -1.0f);
                moved[1]++;
                moved[0]--;
                break;

            case 2:
                movVector = new Vector2(-1.0f, 0.0f);
                moved[2]++;
                moved[3]--;

                break;

            case 3:
                movVector = new Vector2(1.0f, 0.0f);
                moved[3]++;
                moved[2]--;


                break;
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
        if (distanceToPlayer < followTreshold || isAttacking)
        {
            movVector = new Vector2(0.0f, 0.0f);
            if (!isAttacking)
            {
                StartCoroutine(GetComponent<EnemyAttack>().Attack(stats.attack, player.transform.position));
            }
        }
        else
        {
            movVector = player.transform.position - this.transform.position;


        }

        rb.MovePosition(rb.position + movVector * stats.speed / 1.5f * Time.fixedDeltaTime);



    }
    /*
    public void playEnemyStepSound()
    {
        string[] sounds = { "enemy_footstep_1", "enemy_footstep_2", "enemy_footstep_3" };
        print("foi");
        AudioManager.instance.playMultipleRandomSounds(sounds);
    }*/
    /*
    private IEnumerator EnemyStepSoundCooldown(float cooldown)
    {
        canPlayStep = false;

        yield return new WaitForSeconds(cooldown);

        canPlayStep = true;
    }*/
}
