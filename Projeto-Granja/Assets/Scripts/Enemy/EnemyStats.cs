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
    //TODO: Criar fun��o dinamica pra nao ficar repetindo c�digo
    private void playDamageSound()
    {
        string soundType1, soundType2;
        soundType1 = "enemy_damage_1";
        soundType2 = "enemy_damage_2";

        int index = Random.Range(0, 3);
        if (index == 0)
            AudioManager.instance.Play(soundType1);
        else
        {
            AudioManager.instance.Play(soundType2);
        }
        AudioManager.instance.Play("enemy_hit_feedback");
    }
    //TODO: Criar fun��o dinamica pra nao ficar repetindo c�digo
    private void playDeadSound()
    {
        string soundType1, soundType2;
        soundType1 = "enemy_dead_1";
        soundType2 = "enemy_dead_2";

        int index = Random.Range(0, 2);
        if (index == 0)
            AudioManager.instance.Play(soundType1);
        else
        {
            AudioManager.instance.Play(soundType2);
        }
    }

    public void TakeDamage(int damage)
    {
        playDamageSound();
        health -= damage;

        if (health <= 0)
        {
            playDeadSound();
            GetComponent<ItemDrop>().DropItem();
            Destroy(gameObject);
        }
    }
}
