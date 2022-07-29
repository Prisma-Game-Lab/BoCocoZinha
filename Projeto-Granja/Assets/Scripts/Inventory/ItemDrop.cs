using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemDropado;
    public Sprite[] itemSprites;

    public void DropItem()
    {
        int index = Random.Range(0, itemSprites.Length);
        itemDropado.GetComponent<SpriteRenderer>().sprite = itemSprites[index];
        GameObject item = Instantiate(itemDropado, gameObject.transform.position, Quaternion.identity);
    }
}
