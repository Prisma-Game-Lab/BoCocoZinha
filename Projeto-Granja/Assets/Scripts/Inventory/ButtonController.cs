using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button craftRation;
    [SerializeField] private Button craftSebo;
    [SerializeField] private Button craftCake;
    [SerializeField] private Button useRation;
    [SerializeField] private Button useSebo;
    [SerializeField] private Button activateRecipe;
    private PlayerInventory pi;


    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtons();
    }

    private void CheckButtons()
    {
        if(pi.inventory.cornCount > 0 && pi.inventory.brocolliCount > 0 && pi.inventory.carrotCount > 0)
        {
            craftRation.interactable = true;
        }
        else
        {
            craftRation.interactable = false;
        }

        if(pi.inventory.supCount > 0 && pi.inventory.beetCount > 0 && pi.inventory.pumpkinCount > 0)
        {
            craftSebo.interactable = true;
        }
        else
        {
            craftSebo.interactable = false;
        }
        
        if(pi.inventory.strawberryCount > 0 && pi.inventory.bananaCount > 0 && pi.inventory.wheatCount > 0)
        {
            craftCake.interactable = true;
        }
        else
        {
            craftCake.interactable = false;
        }

        if(pi.inventory.rationCount > 0)
        {
            useRation.interactable = true;
        }
        else
        {
            useRation.interactable = false;
        }

        if(pi.inventory.seboCount > 0)
        {
            useSebo.interactable = true;
        }
        else
        {
            useSebo.interactable = false;
        }

        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            activateRecipe.interactable = false;
        }
        else
        {
            activateRecipe.interactable = true;
        }
    }

    public void Cook(string name)
    {
        pi.Cook(name);
    }

    public void UseItem(string name)
    {
        pi.UseItem(name);
    }
}
