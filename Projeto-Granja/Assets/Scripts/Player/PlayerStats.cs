using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int attack;
    public int defense;
    public int speed;
    [HideInInspector] public bool invincible;
    public float invincibilityDuration;

    public GameObject DeathPanel;

    private float invincibleTimer;

    private AudioManager audioManager;

    public bool inCyclone;

    public GameObject camAnchor;
    public GameObject anchor;
    public CinemachineVirtualCamera _camera;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        invincible = false;
        invincibleTimer = -1;
        audioManager = GameObject.FindGameObjectWithTag("PersistentData").GetComponent<AudioManager>();
        slider = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Slider>();
        SetMaxValue(health);
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
        }
        else if (invincible && invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
        else if (invincibleTimer <= 0 && invincibleTimer != -1)
        {
            invincibleTimer = -1;
            invincible = false;
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            audioManager.Play("player_dead");
            Destroy(this.gameObject);
            //DeathPanel.SetActive(true);
            slider.value = 0;
            Time.timeScale = 0;
        } 
        else
        {
            slider.value = health;
        }
    }

    public void Anchor(float x, float y)
    {
        if (anchor != this.gameObject)
        {
            Destroy(anchor.gameObject);
        }
        anchor = Instantiate(camAnchor, new Vector2(x, y), Quaternion.identity);
        _camera = GameObject.FindGameObjectWithTag("Virtual Cam").GetComponent<CinemachineVirtualCamera>();
        _camera.Follow = anchor.transform;
        _camera.LookAt = anchor.transform;
    }

    private void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
}
