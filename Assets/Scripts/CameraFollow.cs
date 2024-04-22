using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {   
        // if player is null
        if (!player) return;

        tempPos = transform.position;
        tempPos.x = player.position.x;
        transform.position = tempPos;
        
    }
}
