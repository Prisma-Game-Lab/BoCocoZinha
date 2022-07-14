using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool canAttack;
    public float range;
    public float attackWindup;
    public float attackTime;
    public float attackCooldown;
    //public Transform attackPoint;
    public LayerMask playerLayer;

    private void Start() 
    {
        canAttack = true;
    }

    public IEnumerator Attack(int damage, Transform attackPoint)
    {
        if(canAttack)
        {
            Debug.Log("Armou");
            StartCoroutine(AttackCooldown());

            yield return new WaitForSeconds(attackWindup);
            Debug.Log("Pei no galo");
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, range, playerLayer);
            StartCoroutine(MovementStop());

            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerStats>().health -= damage;
                player.GetComponent<PlayerStats>().invincible = true;
                Debug.Log(player.GetComponent<PlayerStats>().health);
            }
        }
    }

    private IEnumerator MovementStop()
    {
        GetComponent<EnemyMovement>().isAttacking = true;

        yield return new WaitForSeconds(attackTime);

        GetComponent<EnemyMovement>().isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        Debug.Log("cd 0");

        canAttack = false;

        yield return new WaitForSeconds(attackCooldown + attackWindup + attackTime);
        Debug.Log("cd 1");

        canAttack = true;
    }
}
