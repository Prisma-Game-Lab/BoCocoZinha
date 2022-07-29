using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private CinemachineVirtualCamera _camera;
    [SerializeField] private bool isOnPlayer = false;
    void Start()
    {
        
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
            _camera = GameObject.FindGameObjectWithTag("Virtual Cam").GetComponent<CinemachineVirtualCamera>();
            if (_camera != null)
            {
                isOnPlayer = true;
                Debug.Log(transform.position);
                _camera.Follow = transform;
                _camera.LookAt = transform;
            }
            //}
        }
    }
}
