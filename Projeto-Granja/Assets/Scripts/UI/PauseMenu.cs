using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject inventoryMenuUI;
    [SerializeField] private GameObject settingsMenuUI;

    [SerializeField] private GameObject player;

    void OnPause()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        if(paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void OnInventory()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(paused)
        {
            ResumeGame();
        }
        else
        {
            generalPanel.SetActive(true);
            inventoryMenuUI.SetActive(true);
            Time.timeScale = 0.0f;
            paused = true;

            if(player != null)
            {
                player.GetComponent<PlayerInput>().enabled = false;
            }
        }
    }

    public void PauseGame()
    {
        generalPanel.SetActive(true);
        settingsMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        paused = true;

        if(player != null)
        {
            player.GetComponent<PlayerInput>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        inventoryMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        generalPanel.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;

        if(player != null)
        {
            player.GetComponent<PlayerInput>().enabled = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
        paused = false;
    }
}
