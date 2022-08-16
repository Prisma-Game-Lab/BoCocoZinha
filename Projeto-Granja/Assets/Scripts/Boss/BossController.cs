using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private int bossHP;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float laserDuration;
    [SerializeField] private float laserAnticipation;

    private bool canAttack;

    [SerializeField] private BossCyclone bossCyclone;
    [SerializeField] private BossGroundPound bossGroundPound;
    [SerializeField] private LaserController laserController;
    
    // Start is called before the first frame update
    void Start()
    {
        canAttack = false;

        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            StartAttack();
        }
    }

    public void StartAttack()
    {
        int index = Random.Range(0, 3);

        if(index == 0)
        {
            bossCyclone.Activate();
        }
        else if(index == 1)
        {
            bossGroundPound.Activate();
        }
        else
        {
            laserController.ActivateLaser(laserDuration, laserAnticipation);
        }

        canAttack = false;
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("timing");
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        bossHP -= damage;

        if (bossHP <= 0)
        {
            GetComponent<ItemDrop>().DropItem();
            Destroy(gameObject);
        }
    }
}
