using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTrigger : MonoBehaviour
{
    public int sceneIndex;

    public void ChangeScene()
    {
        GetComponent<LevelLoader>().LoadLevel();
        //SceneManager.LoadScene(sceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            ChangeScene();
        }    
    }
}
