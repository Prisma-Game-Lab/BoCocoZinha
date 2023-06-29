using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public Collider2D hurtbox;
    public PlayerStats stats;
    public int collisionDamage;
    public int laserDamage;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("PersistentData").GetComponent<AudioManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyAttack"))
        {
            if (stats.invincible == false && playerMovement.isDashing == false)
            {
                stats.Hit(collisionDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            if (stats.invincible == false && playerMovement.isDashing == false)
            {
                Debug.Log($"{other.gameObject.name}",other.gameObject);
                stats.Hit(laserDamage);

            }
        }
    }
}
