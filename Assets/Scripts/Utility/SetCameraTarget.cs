using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraTarget : MonoBehaviour
{
    Player player;
    CinemachineVirtualCamera vcam;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        vcam = GetComponent<CinemachineVirtualCamera>();

        vcam.Follow = player.gameObject.transform;
        vcam.LookAt = player.gameObject.transform;
    }
}
