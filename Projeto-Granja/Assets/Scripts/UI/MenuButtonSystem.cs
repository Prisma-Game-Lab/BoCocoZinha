using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonSystem : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Playtest Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
