using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCyclone : MonoBehaviour
{
    [SerializeField] private GameObject damageArea;
    [SerializeField] private float duration;
    [SerializeField] private int damage;
    [SerializeField] private float damageTimer;
    private float baseDamageTimer;
    private bool isActive;
    private GameObject player;
    private Animator animator;
    private BossController bc;

    // Start is called before the first frame update
    void Start()
    {
        baseDamageTimer = damageTimer;
        animator = GetComponent<Animator>();
        isActive = false;
        bc = GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(player != null && player.GetComponent<PlayerStats>().inCyclone == true && damageTimer <= 0 && isActive)
        {
            DealDamage();
            damageTimer = baseDamageTimer;
        }

        if(damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    public void Activate()
    {
        StartCoroutine(ActiveTime());
    }

    private IEnumerator ActiveTime()
    {
        damageArea.SetActive(true);
        animator.SetInteger("Cyclone", 1);
        isActive = true;

        yield return new WaitForSeconds(duration);

        damageArea.SetActive(false);
        animator.SetInteger("Cyclone", 0);
        isActive = false;
        StartCoroutine(bc.AttackCooldown());

        if(player.GetComponent<PlayerStats>().inCyclone)
        {
            player.GetComponent<PlayerStats>().inCyclone = false;
        }

    }

    private void DealDamage()
    {
        player.GetComponent<PlayerStats>().health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isActive && other.CompareTag("Player") && player.GetComponent<PlayerStats>().inCyclone == false)
        {
            player.GetComponent<PlayerStats>().inCyclone = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && player.GetComponent<PlayerStats>().inCyclone == true)
        {            
            player.GetComponent<PlayerStats>().inCyclone = false;
        }  
    }


}
