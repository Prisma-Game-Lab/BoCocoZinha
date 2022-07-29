using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomLocker : MonoBehaviour
{
    public List<GameObject> enemies;
    private bool locked;
    public List<GameObject> doors;
    // Start is called before the first frame update
    void Start()
    {
        locked = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            CheckEnemies();
        }
    }
    
    private void CheckEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return;
            }

        }
        OpenDoor();
        return ;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CloseDoor();
        }
    }
    public void CloseDoor()
    {
        locked = true;
        for (int i=0;i<doors.Count;i++) //(initial valor;condition;adition per frame)
        {
            doors[i].gameObject.SetActive(true);
        }
    }
    public void OpenDoor()
    {
        locked = false;
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].gameObject.SetActive(false);
        }
    }
}

