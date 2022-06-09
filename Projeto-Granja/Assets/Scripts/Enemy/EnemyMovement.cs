using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool attacking;
    private Rigidbody2D rb;

    private float moveTimer;
    private int lastDirection; //0-Up, 1-Down, 2-Left, 3-Right
    private int newDirection;
    private Vector2 movVector;

    public int movLimit;
    private int[] moved = new int[4];

    public EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        lastDirection = 0;

        moved[0] = 0; //up
        moved[1] = 0; //down
        moved[2] = 0; //left
        moved[3] = 0; //right
    }

    // Update is called once per frame
    void Update()
    { 
        if (attacking == false)
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

                Debug.Log("New Direction " + newDirection.ToString());
                moveTimer += Time.deltaTime;
                rb.MovePosition(rb.position + movVector * stats.speed * Time.fixedDeltaTime);
            }
            else
            {
                moveTimer += Time.deltaTime;
                rb.MovePosition(rb.position + movVector * stats.speed * Time.fixedDeltaTime);
            }
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
}
