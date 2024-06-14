using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    
    [SerializeField] public float minX, maxX, minY, maxY;

    [SerializeField] private bool moveOnY;
    public bool playerDependant;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerDependant = true;
    }

    void LateUpdate()
    {   
        if (playerDependant)
            MoveCameraWithPlayer();
    }

    void MoveCameraWithPlayer()
    {
        // if player is null
        if (!player) 
        {
            print("can't find player");
            return;
        }
        tempPos = transform.position;
        tempPos.x = player.position.x;
        if (moveOnY)
        {
            tempPos.y = player.position.y;
        }

        // move camera if camera is in rangea
        if (tempPos.x > minX && tempPos.x < maxX) 
            transform.position = new (tempPos.x, transform.position.y, transform.position.z);

        if (player.position.y > minY && player.position.y < maxY) 
            transform.position = new (transform.position.x, tempPos.y, transform.position.z);
        
    }
}