using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraBoss : MonoBehaviour
{
    public float posX;
    public float posY;
    // Start is called before the first frame update
    void Start()
    {
        posX = this.gameObject.transform.position.x - 4.0f;
        posY = this.gameObject.transform.position.y + 2.0f;
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
            player.GetComponent<PlayerStats>().Anchor(posX, posY);

        }
    }
}
