using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
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
            inventory.cakeCount++;
        }
        else if (recipe=="Ration")
        {
            inventory.rationCount++;
        }
        else
        {
            inventory.seboCount++;
        }
    }
}
