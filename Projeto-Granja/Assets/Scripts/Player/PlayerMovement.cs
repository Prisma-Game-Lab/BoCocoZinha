using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movX;
    private float movY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue mov)
    {
        Vector2 movVector = mov.Get<Vector2>();
        movX = movVector.x;
        movY = movVector.y;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(5*movX, 5*movY));
    }
}
