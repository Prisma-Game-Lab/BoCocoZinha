using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int attack;
    public int defense;
    public int speed;
    [HideInInspector] public bool invincible;
    public float invincibilityDuration;

    public Text hpText;

    public GameObject DeathPanel;

    private float invincibleTimer;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        invincible = false;
        invincibleTimer = -1;
        hpText = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInvincible();
        CheckHealth();
    }

    private void UpdateInvincible()
    {
        if (invincible && invincibleTimer == -1)
        {
            invincibleTimer = invincibilityDuration;
            Debug.Log("Inicio invencivel");
        }
        else if (invincible && invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
        else if (invincibleTimer <= 0 && invincibleTimer != -1)
        {
            invincibleTimer = -1;
            invincible = false;
            Debug.Log("Fim invencivel");
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            DeathPanel.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            hpText.text = "Player Health: " + health.ToString();
        }
    }
}
