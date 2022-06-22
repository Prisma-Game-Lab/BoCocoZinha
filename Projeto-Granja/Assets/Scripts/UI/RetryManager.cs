using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{

    public void Retry()
    {
        string atual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(atual);
    }
}
