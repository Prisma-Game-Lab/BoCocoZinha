using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.Play("boss_scream");
            StartCoroutine(boss.GetComponent<BossController>().AttackCooldown());
        }    
    }
}
