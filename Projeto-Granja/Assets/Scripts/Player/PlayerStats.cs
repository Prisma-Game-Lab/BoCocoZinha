using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int attack;
    public int defense;
    public int speed;
    [HideInInspector] public bool invincible;
    public float invincibilityDuration;
    public int rationHeal;

    public GameObject DeathPanel;

    private float invincibleTimer;
    private int maxHealth;

    private AudioManager audioManager;

    public bool inCyclone;

    public GameObject camAnchor;
    public GameObject anchor;
    public CinemachineVirtualCamera _camera;

    public bool isSpeedUp;

    private Slider slider;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Time.timeScale = 1;
        invincible = false;
        invincibleTimer = -1;
        audioManager = GameObject.FindGameObjectWithTag("PersistentData").GetComponent<AudioManager>();
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            slider = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Slider>();
            SetMaxValue(health);
        }
        maxHealth = health;
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            HPManager.currentHP = maxHealth;
        }
        else
        {
            health = HPManager.currentHP;
        }
    }

    public void Hit(int damage)
    {
        if (!invincible)
        {
            Debug.Log($"tomei dano!");
            audioManager.Play("player_hit");
            health -= damage;
            invincible = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInvincible();

        if (slider)
        {
            CheckHealth();
        }
    }

    private void UpdateInvincible()
    {
        if (invincible && invincibleTimer == -1)
        {
            invincibleTimer = invincibilityDuration;
            animator.SetBool("Invincible", true);
        }
        else if (invincible && invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
        else if (invincibleTimer <= 0 && invincibleTimer != -1)
        {
            invincibleTimer = -1;
            invincible = false;
            animator.SetBool("Invincible", false);

        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            audioManager.Play("player_dead");
            Destroy(this.gameObject);
            //DeathPanel.SetActive(true);
            SceneManager.LoadScene("Grange");
        }
        else
        {
            slider.value = health;
        }
        HPManager.currentHP = health;
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

    public void healPlayer()
    {
        if (health + rationHeal > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += rationHeal;
        }
    }

    public void speedUpPlayer()
    {
        if (!isSpeedUp)
        {
            isSpeedUp = true;
            var mov = GetComponent<PlayerMovement>();
            mov.moveSpeed = mov.moveSpeed*2f;
            StartCoroutine(EndSpeedup(mov));
        }
    }
    IEnumerator EndSpeedup(PlayerMovement mov)
    {
        yield return new WaitForSeconds(40f);
        isSpeedUp=false;
        mov.moveSpeed/=2f;
    }
}
