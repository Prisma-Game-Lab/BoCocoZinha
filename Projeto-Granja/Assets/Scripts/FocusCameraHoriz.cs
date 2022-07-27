using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraHoriz : MonoBehaviour
{
    private float lenght;
    // Start is called before the first frame update
    void Start()
    {
        lenght = this.gameObject.transform.position.x;
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
            if (player.transform.position.x > (lenght - 5.5f))
            {
                Debug.Log("da esquerda");
                player.GetComponent<PlayerStats>().currentRoomX += 19;
            }
            else
            {
                Debug.Log("da direita");
                player.GetComponent<PlayerStats>().currentRoomX -= 19;
            }
        }
    }
}
