using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemTypes
{
    STRAWBERRY,
    CORN,
    CARROT,
    BEET,
    BROCOLLI,
    WHEAT,
    SUP,
    PUMPKIN,
    BANANA
}
public class Item : MonoBehaviour
{
    public itemTypes type;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerInventory>().AddToInventory(this);
        }
    }
}
