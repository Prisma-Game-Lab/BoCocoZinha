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
    private AudioManager audioManager;
    private Animator animator;

    private void Start() 
    {
        canAttack = true;
        audioManager = GameObject.FindGameObjectWithTag("PersistentData").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    public IEnumerator Attack(int damage, Vector3 attackPoint)
    {
        if(canAttack)
        {
            StartCoroutine(AttackCooldown());
            StartCoroutine(MovementStop());

            yield return new WaitForSeconds(attackWindup);
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint, range, playerLayer);

            foreach (Collider2D player in hitPlayer)
            {
                audioManager.Play("player_hit");
                player.GetComponent<PlayerStats>().health -= damage;
                player.GetComponent<PlayerStats>().invincible = true;
            }
        }
    }

    private IEnumerator MovementStop()
    {
        GetComponent<EnemyMovement>().isAttacking = true;
        animator.SetBool("Attacking", true);

        yield return new WaitForSeconds(attackTime + attackWindup);

        GetComponent<EnemyMovement>().isAttacking = false;
        animator.SetBool("Attacking", false);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown + attackWindup + attackTime);

        canAttack = true;
    }
    private void playEnemyAttackSound()
    {
        string[] sounds = { "enemy_swing_1", "enemy_swing_2", "enemy_swing_3" };
        AudioManager.instance.playMultipleRandomSounds(sounds);
    }
}
