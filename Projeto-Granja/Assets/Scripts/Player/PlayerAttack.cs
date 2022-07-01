using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float normalCooldown;
    public int normalDamage;
    public float chargedCooldown;
    public int chargedDamage;
    public float range;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    private bool canAttack;

    private void Start() 
    {
        canAttack = true;
    }

    public void OnNormalAttack() 
    {
        Attack(normalDamage, normalCooldown);
    }

    public void OnChargedAttack() 
    {
        Attack(chargedDamage, chargedCooldown);
    }

    private void Attack(int damage, float cooldown)
    {
        if(canAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {    
                enemy.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            StartCoroutine(Cooldown(cooldown));
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

    private IEnumerator Cooldown(float cooldown)
    {
        canAttack = false;

        yield return new WaitForSeconds(cooldown); 

        canAttack = true;
    }
}
