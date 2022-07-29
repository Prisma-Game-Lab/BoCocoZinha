using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] private string levelToGo;
    private GameObject loadingScreen;
    private Slider slider;


    public void Start()
    {
        loadingScreen = GameObject.FindGameObjectWithTag("Loading Screen");
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        //FindObjectOfType<AudioManager>().StopAllSounds();
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        loadingScreen.transform.GetChild(0).gameObject.SetActive(true);

        slider = loadingScreen?.transform.GetChild(0).GetChild(0).GetComponent<Slider>();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
