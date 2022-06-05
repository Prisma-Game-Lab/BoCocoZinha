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
    public int MoveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue mov)
    {
        movVector = mov.Get<Vector2>();
        movX = movVector.x;
        movY = movVector.y;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movVector * MoveSpeed * Time.fixedDeltaTime);
    }
}
