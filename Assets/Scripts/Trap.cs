using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //public static bool hasTrapMoved;
    [SerializeField]
    private float finalXPos, finalYPos, initialXPos, initialYPos, moveSpeed, waitTime;
    Vector2 tempPos;
    void Awake()
    {
        initialXPos = transform.position.x;
        initialYPos = transform.position.y; 
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
                hasFinishedMoving = true;
            }
            yield return null;
        }
    }

    public IEnumerator TemporaryMoveTrap()
    {
        bool hasFinishedMoving1 = false;

        // trap going up
        while(!hasFinishedMoving1)
        {
            Debug.Log($"current pos: ({transform.position.x}, {transform.position.y}), final pos: ({finalXPos}, {finalYPos})");
            tempPos = transform.position;
            if (transform.position.x <= finalXPos) tempPos.x += 0.1f * moveSpeed;
            if (transform.position.y <= finalYPos) tempPos.y += (float) 0.1 * moveSpeed;
            transform.position = tempPos;
            
            // if trap has reached final position, then stop moving and stop coroutine
            if (transform.position.x >= finalXPos && transform.position.y >= finalYPos) 
            {
                hasFinishedMoving1 = true;
            }

            yield return null;
            //else hasFinishedMoving1 = true;
        }

        bool hasFinishedMoving2 = false;
        Debug.Log("start 1s");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("end 1s");

        // trap going back to original position
        while (!hasFinishedMoving2)
        {
            Debug.Log("temp move coming down");
            tempPos = transform.position;
            if (transform.position.x >= initialXPos) tempPos.x -= 0.1f * moveSpeed;
            if (transform.position.y >= initialYPos) tempPos.y -= (float) 0.1 * moveSpeed;
            transform.position = tempPos;

            if (transform.position.x <= initialXPos && transform.position.y <= initialYPos) 
            {
                hasFinishedMoving2 = true;
            }

            yield return null;
            
        }
    }
}
