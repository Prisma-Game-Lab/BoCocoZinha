using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    private float height;
    public float posX;
    public float posY;
    // Start is called before the first frame update
    void Start()
    {
        posX = this.gameObject.transform.position.x - 3.0f;
        posY = this.gameObject.transform.position.y - 0.5f;
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
            if (player.transform.position.y > (posY))
            {
                Debug.Log("de baixo");
                //player.GetComponent<PlayerStats>().currentRoomY += 13;
                player.GetComponent<PlayerStats>().Anchor(posX, posY + 6.5f);
            } else
            {
                Debug.Log("de cima");
                //player.GetComponent<PlayerStats>().currentRoomY -= 13;
                player.GetComponent<PlayerStats>().Anchor(posX, posY - 6.5f);
            }
        }
    }
}
