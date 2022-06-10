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
            _camera.Follow = GameObject.FindGameObjectWithTag("PlayerAttack").transform;
            _camera.LookAt = GameObject.FindGameObjectWithTag("PlayerAttack").transform;
            isOnPlayer = true;
        }
    }
}
