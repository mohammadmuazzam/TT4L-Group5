using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static bool hasTrapMoved;
    [SerializeField]
    private float moveAmountX, moveAmountY, moveSpeed;
    private float finalXPos, finalYPos;
    Vector2 tempPos;
    void Awake()
    {
        finalXPos = transform.position.x + moveAmountX;
        finalYPos = transform.position.y + moveAmountY;
    }

    // move trap
    public IEnumerator PermanentMoveTrap()
    {
        bool hasFinishedMoving = false;
        while(!hasFinishedMoving)
        {
            tempPos = transform.position;
            if (transform.position.x <= finalXPos) tempPos.x += 0.1f * moveSpeed;
            if (transform.position.y <= finalYPos) tempPos.y += (float) 0.1 * moveSpeed;
            transform.position = tempPos;
            
            // if trap has reached final position, then stop moving and stop coroutine
            if (transform.position.x >= finalXPos && transform.position.y >= finalYPos) 
            {
                hasTrapMoved = true;
                hasFinishedMoving = true;
            }
            yield return null;
        }
    }
}
