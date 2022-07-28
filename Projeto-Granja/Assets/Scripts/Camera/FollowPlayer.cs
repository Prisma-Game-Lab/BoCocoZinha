using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera _camera;
    private bool isOnPlayer = false;
    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnPlayer)
        {
            /*if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().anchor != null)
            {
                _camera.Follow = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().anchor.transform;
                _camera.LookAt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().anchor.transform;
                isOnPlayer = true;
                Debug.Log("aaaaaaaaaa");
            } else
            {*/

                _camera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
                _camera.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
                isOnPlayer = true;
            //}
        }
    }
}
