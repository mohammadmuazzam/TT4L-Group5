using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    [SerializeField]
    private float minX, maxX;

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

        // move camera if camera is in the range
        if (tempPos.x > minX && tempPos.x < maxX) transform.position = tempPos;
        
    }
}
