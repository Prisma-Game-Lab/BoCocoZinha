using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private EnemyMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponentInParent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement.attacking = true;
            Debug.Log("visao");
        }
    }
}
