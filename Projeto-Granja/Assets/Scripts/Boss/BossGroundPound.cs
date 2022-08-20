using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundPound : MonoBehaviour
{
    [SerializeField] private GameObject damageArea;
    [SerializeField] private float duration;
    [SerializeField] private int damage;
    [SerializeField] private float increase;
    private GameObject player;
    private float minRadius;
    private bool isActive;
    private CircleCollider2D collider;
    private BossController bc;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        collider = damageArea.gameObject.GetComponent<CircleCollider2D>();    
        minRadius = collider.radius;
        isActive = false;
        bc = GetComponent<BossController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(isActive)
        {
            collider.radius += increase * Time.deltaTime;
        }
    }

    public void Activate()
    {
        StartCoroutine(ActiveTime());
    }

    private IEnumerator ActiveTime()
    {
        isActive = true;
        damageArea.SetActive(true);
        animator.SetInteger("Pound", 1);
        AudioManager.instance.Play("boss_stomp");

        yield return new WaitForSeconds(duration);

        collider.radius = minRadius;
        damageArea.SetActive(false);
        isActive = false;
        animator.SetInteger("Pound", 0);
        StartCoroutine(bc.AttackCooldown());
    }

    private void DealDamage()
    {
        AudioManager.instance.Play("player_hit");
        player.GetComponent<PlayerStats>().health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            DealDamage();
        }    
    }
}
