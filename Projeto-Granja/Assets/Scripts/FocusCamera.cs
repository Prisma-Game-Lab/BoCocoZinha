using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        height = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            if (player.transform.position.y > height)
            {
                Debug.Log("de baixo");
                player.GetComponent<PlayerStats>().currentRoomY += 13;
            } else
            {
                Debug.Log("de cima");
                player.GetComponent<PlayerStats>().currentRoomY -= 13;
            }
        }
    }
}
