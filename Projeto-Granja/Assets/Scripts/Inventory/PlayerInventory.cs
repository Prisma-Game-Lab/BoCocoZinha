using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
    private GameObject player;

    private void Update() 
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void AddToInventory(Item item)
    {
        switch (item.type)
        {
            case itemTypes.STRAWBERRY:
                inventory.strawberryCount++;
                break;
            case itemTypes.CORN:
                inventory.cornCount++;
                break;
            case itemTypes.CARROT:
                inventory.carrotCount++;
                break;
            case itemTypes.BEET:
                inventory.beetCount++;
                break;
            case itemTypes.BROCOLLI:
                inventory.brocolliCount++;
                break;
            case itemTypes.WHEAT:
                inventory.wheatCount++;
                break;
            case itemTypes.SUP:
                inventory.supCount++;
                break;
            case itemTypes.PUMPKIN:
                inventory.pumpkinCount++;
                break;
            case itemTypes.BANANA:
                inventory.bananaCount++;
                break;
            default:
                break;
        }
    }

    public void Cook(string recipe)
    {
        if (recipe=="Cake")
        {
            inventory.bananaCount--;
            inventory.wheatCount--;
            inventory.strawberryCount--;
            inventory.cakeCount++;
            SceneManager.LoadScene("End");
        }
        else if (recipe=="Ration")
        {
            inventory.cornCount--;
            inventory.carrotCount--;
            inventory.brocolliCount--;
            inventory.rationCount++;
        }
        else
        {
            inventory.supCount--;
            inventory.beetCount--;
            inventory.pumpkinCount--;
            inventory.seboCount++;
        }
    }

    public void UseItem(string item)
    {
        if(item == "Ration")
        {
            inventory.rationCount--;
            player.GetComponent<PlayerStats>().healPlayer();
        }
        else
        {
            inventory.seboCount--;
            player.GetComponent<PlayerStats>().speedUpPlayer();
        }
    }
}
