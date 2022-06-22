using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonSystem : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public AudioManager audioManager;

    public void Play()
    {
        audioManager.Play("button_in");
        SceneManager.LoadScene("Playtest Scene");
    }

    public void OpenSettings()
    {
        audioManager.Play("button_in");
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        audioManager.Play("button_out");
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        audioManager.Play("button_in");
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        audioManager.Play("button_out");
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.Play("button_out");
        Application.Quit();
    }
}
