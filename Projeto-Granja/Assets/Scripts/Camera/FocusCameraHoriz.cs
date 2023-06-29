using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraHoriz : MonoBehaviour
{
    private float lenght;
    public float posX;
    public float posY;
    // Start is called before the first frame update
    void Start()
    {
        posX = this.gameObject.transform.position.x - 5.5f;
        posY = this.gameObject.transform.position.y - 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     GameObject player = collision.gameObject;
        //     if (player.transform.position.x > (posX))
        //     {
        //         Debug.Log("da esquerda");
        //         //centro do corredor ate centro da sala a esquerda
        //         player.GetComponent<PlayerStats>().Anchor(posX + 9.5f, posY);
        //     }
        //     else
        //     {
        //         Debug.Log("da direita");
        //         //centro do corredor ate centro da sala a esquerda
        //         player.GetComponent<PlayerStats>().Anchor(posX - 9.5f, posY);
        //     }
        // }
    }
}
