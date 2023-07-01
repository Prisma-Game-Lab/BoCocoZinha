using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int attack;
    public int defense;
    public int speed;

    //public Text hpEnemyText
    private void playEnemyDamageSound(bool isChargedAttack)
    {
        string[] sounds = { "enemy_damage_1", "enemy_damage_2" };
        AudioManager.instance.playMultipleRandomSounds(sounds);
        if (isChargedAttack)
            AudioManager.instance.Play("enemy_chargedhit_feedback");
        else
            AudioManager.instance.Play("enemy_hit_feedback");
    }

    private void playEnemyDeadSound()
    {
        string[] sounds = { "enemy_dead_1", "enemy_dead_2" };
        AudioManager.instance.playMultipleRandomSounds(sounds);
        AudioManager.instance.Play("enemy_hit_feedback");
    }

    public void TakeDamage(int damage, bool isChargedAttack)
    {
        playEnemyDamageSound(isChargedAttack);
        StartCoroutine(Flash());
        health -= damage;

        if (health <= 0)
        {
            playEnemyDeadSound();
            GetComponent<ItemDrop>().DropItem();
            Destroy(gameObject);
        }
    }
    IEnumerator Flash()
    {
        var anim = GetComponent<Animator>();
        if(anim == null){
            Debug.Log($"anim not found");
            yield break;
        }
        anim.SetBool("Flashing", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Flashing", false);
    }
}
