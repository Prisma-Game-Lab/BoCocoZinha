using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] itemDropado;
    


    public void DropItem()
    {
        int index = Random.Range(0, itemDropado.Length);
        GameObject item = Instantiate(itemDropado[index], gameObject.transform.position, Quaternion.identity);
    }
}
