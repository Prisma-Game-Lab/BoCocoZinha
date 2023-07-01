using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private int bossHP;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float laserDuration;
    [SerializeField] private float laserAnticipation;

    public bool canAttack;

    [SerializeField] private BossCyclone bossCyclone;
    [SerializeField] private BossGroundPound bossGroundPound;
    [SerializeField] private LaserController laserController;
    [SerializeField] private GameObject portal;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canAttack = false;

        //StartCoroutine(AttackCooldown());
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
        animator.SetTrigger("Flash");
        bossHP -= damage;

        if (bossHP <= 0)
        {
            AudioManager.instance.Play("boss_dead");
            GetComponent<ItemDrop>().DropItem();
            portal.SetActive(true);
            Destroy(gameObject);
        }
        else
            AudioManager.instance.Play("boss_damage");
    }
}
