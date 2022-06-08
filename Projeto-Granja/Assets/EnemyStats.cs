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

    public Text hpEnemyText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hpEnemyText.text = "Health: " + health.ToString();
    }
}
