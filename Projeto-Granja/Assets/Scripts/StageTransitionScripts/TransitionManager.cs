using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(2);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelIndex);
    }
    }