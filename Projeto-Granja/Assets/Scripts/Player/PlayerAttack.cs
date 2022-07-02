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

    private bool canNormalAttack;
    private bool canChargedAttack; 

    private void Start() 
    {
        canNormalAttack = true;
        canChargedAttack = true;
    }

    public void OnNormalAttack() 
    {
        if(canNormalAttack)
        {
            Attack(normalDamage, normalCooldown); 
            StartCoroutine(NormalCooldown(normalCooldown));
        }
    }

    public void OnChargedAttack() 
    {
        if(canChargedAttack)
        {
            Attack(chargedDamage, chargedCooldown);
            StartCoroutine(ChargedCooldown(chargedCooldown));
        }
    }

    private void Attack(int damage, float cooldown)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {    
            enemy.GetComponent<EnemyStats>().TakeDamage(damage);
        }
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

    private void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}
