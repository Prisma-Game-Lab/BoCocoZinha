using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public List<TextMeshProUGUI> texts;
    public TextMeshProUGUI text;
    public GameObject button;
    public string scene;

    private int index;
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        text.text = texts[index].text;

        EventSystem.current.SetSelectedGameObject(button);

        am = GameObject.FindGameObjectWithTag("PersistentData").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNormalAttack(){
        ShowText();
    }
    public void ShowText(){
        if (index < texts.Count - 1){
            index++;
            text.text = texts[index].text;
            am.Play("turn_page");
        }
        else if (index == texts.Count -1 ){
            SceneManager.LoadScene(scene);
        }
    }
    
}
