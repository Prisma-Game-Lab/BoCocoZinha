using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float normalCooldown;
    public int normalDamage;
    public float chargedCooldown;
    public int chargedDamage;
    public float range;
    public float distance;

    public float knockBackTime;
    public float knockBackDistance;

    public Transform attackPoint;
    private Animator animatorAttackPoint;
    public LayerMask enemyLayer;

    private bool canNormalAttack;
    private bool canChargedAttack;
    private Animator animator;
    public ParticleSystem ps;

    //Animator hashes
    private int animAttack = Animator.StringToHash("Attack");
    private int animMoving = Animator.StringToHash("Moving");

    public float attackTime;
    public bool isAttacking;
    public bool isCharging;

    private void Start()
    {
        canNormalAttack = true;
        canChargedAttack = true;
        animator = GetComponent<Animator>();
        animatorAttackPoint = attackPoint.GetComponent<Animator>();
    }

    /// <summary>
    /// Repositions the AttackPoint based on a given position
    /// </summary>
    /// <param name="position">New position of the AttackPoint</param>
    public void UpdateAttackPoint(Vector2 position)
    {
        if (isAttacking) return;
        attackPoint.localPosition = position.normalized * distance;

        float rotation = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        attackPoint.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void OnNormalAttack()
    {
        if (canNormalAttack && !gameObject.GetComponent<PlayerMovement>().isDashing)
        {
            Attack(normalDamage, false);
            StartCoroutine(NormalCooldown(normalCooldown));
        }
    }

    public void OnCharging()
    {
        StartCoroutine(Charging());
    }

    public void OnCancelChargedAttack()
    {
        StopCoroutine(Charging());
        isCharging = false;
        ps.Stop();
    }

    public void OnChargedAttack()
    {
        if (canChargedAttack && !gameObject.GetComponent<PlayerMovement>().isDashing)
        {
            Attack(chargedDamage, true);
            StartCoroutine(ChargedCooldown(chargedCooldown));
        }
    }

    private IEnumerator AttackCoroutine(int damage, bool isChargedAttack)
    {
        animator.SetTrigger(animAttack);
        animatorAttackPoint.SetTrigger(animAttack);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        isAttacking = true;

        if (isChargedAttack)
            AudioManager.instance.Play("swing_charged");
        else
        {
            string[] sounds = { "swing_normal_1", "swing_normal_2", "swing_normal_3" };
            AudioManager.instance.playMultipleRandomSounds(sounds);
        }

        if (hitEnemies.Length == 0)
        {
            if (isChargedAttack)
                AudioManager.instance.Play("miss_charged_hit");
            else
            {
                AudioManager.instance.Play("miss_normal_hit");
            }
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.CompareTag("Enemy") && !enemy.CompareTag("Breakable") && !enemy.CompareTag("Boss")) continue;

            if (enemy.CompareTag("Enemy"))
            {
                StartCoroutine(KnockBack(enemy.gameObject));
                enemy.GetComponent<EnemyStats>().TakeDamage(damage, isChargedAttack);
            }
            else if (enemy.CompareTag("Boss"))
            {
                enemy.GetComponent<BossController>().TakeDamage(damage);
            }
            else
            {
                Destroy(enemy.gameObject);
                ItemDrop entity = enemy.GetComponent<ItemDrop>();
                entity.DropItem();
                if (entity.entityType == "box")
                {
                    string[] sounds = { "box_breaking_1", "box_breaking_2", "box_breaking_3" };
                    AudioManager.instance.playMultipleRandomSounds(sounds);
                }
                else if (entity.entityType == "plant")
                {
                    string[] sounds = { "gather_plants_1", "gather_plants_2" };
                    AudioManager.instance.playMultipleRandomSounds(sounds);
                }
            }
        }
        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
    }
    private void Attack(int damage, bool isChargedAttack)
    {
        StartCoroutine(AttackCoroutine(damage, isChargedAttack));
    }

    private IEnumerator NormalCooldown(float cooldown)
    {
        canNormalAttack = false;

        yield return new WaitForSeconds(cooldown);

        canNormalAttack = true;
    }

    private IEnumerator ChargedCooldown(float cooldown)
    {
        canChargedAttack = false;

        yield return new WaitForSeconds(cooldown);

        canChargedAttack = true;
    }

    private IEnumerator KnockBack(GameObject enemy)
    {
        Rigidbody2D enemy_rb = enemy.GetComponent<Rigidbody2D>();
        if (enemy_rb == null) yield break;

        enemy.GetComponent<EnemyMovement>().knockback = true;
        enemy_rb.isKinematic = false;
        Vector2 diff = enemy_rb.transform.position - transform.position;
        diff = diff.normalized * knockBackDistance;
        enemy_rb.AddForce(diff, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackTime);
        if (enemy_rb == null) yield break;
        enemy_rb.velocity = Vector2.zero;
        enemy_rb.isKinematic = true;
        enemy.GetComponent<EnemyMovement>().knockback = false;
    }

    private IEnumerator Charging()
    {
        isCharging = true;
        animator.SetBool(animMoving, false);
        ps.Play();

        yield return new WaitForSeconds(1.0f);

        isCharging = false;
        ps.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}
