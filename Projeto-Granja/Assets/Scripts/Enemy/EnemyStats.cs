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

    // Start is called before the first frame update
    void Start()
    {
        //hpEnemyText = GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //hpEnemyText.text = "Enemy Health: " + health.ToString();

    }

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
        health -= damage;

        if (health <= 0)
        {
            playEnemyDeadSound();
            GetComponent<ItemDrop>().DropItem();
            Destroy(gameObject);
        }
    }
}
