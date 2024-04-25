using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //public static bool hasTrapMoved;
    [SerializeField]
    private float moveAmountX, moveAmountY, moveSpeedX, moveSpeedY, waitTime;

    private float finalXPos, finalYPos, initialXPos, initialYPos;
    private bool negativeX, negativeY;
    Vector2 tempPos;
    void Awake()
    {
        initialXPos = transform.position.x;
        initialYPos = transform.position.y; 

        finalXPos = initialXPos + moveAmountX;
        finalYPos = initialYPos + moveAmountY;

        //* check if final position is less than initial position
        if (finalXPos < initialXPos) negativeX = true;
        else negativeX = false;

        if (finalYPos < initialYPos) negativeY = true;
        else negativeY = false;
        
    }

    // move trap
    public IEnumerator PermanentMoveTrap()
    {
        bool hasFinishedMoving = false;
        while(!hasFinishedMoving)
        {
            ActivateTrap();
            
            // if trap has reached final position, then stop moving and stop coroutine
            if ((!negativeX && transform.position.x >= finalXPos) || (negativeX && transform.position.x <= finalXPos))
            {
                if ((!negativeY && transform.position.y >= finalYPos) || (negativeY && transform.position.y <= finalYPos))
                {
                    hasFinishedMoving = true;
                }
            }
            yield return null;
        }
    }

    public IEnumerator TemporaryMoveTrap()
    {
        bool hasFinishedMoving1 = false;

        // trap activating
        while(!hasFinishedMoving1)
        {
            Debug.Log($"{gameObject.name} current pos: ({transform.position.x}, {transform.position.y}), final pos: ({finalXPos}, {finalYPos})");
            ActivateTrap();     
            
            // if trap has reached final position, then stop moving and stop coroutine
            if ((!negativeX && transform.position.x >= finalXPos) || (negativeX && transform.position.x <= finalXPos))
            {
                if ((!negativeY && transform.position.y >= finalYPos) || (negativeY && transform.position.y <= finalYPos))
                {
                    hasFinishedMoving1 = true;
                }
            }

            yield return null;
        }

        bool hasFinishedMoving2 = false;
        Debug.Log("start 1s");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("end 1s");

        // trap going back to original position
        while (!hasFinishedMoving2)
        {
            Debug.Log($"{gameObject.name} temp moving back to original position");
            DeactivateTrap();

            if ((!negativeX && transform.position.x <= initialXPos) || (negativeX && transform.position.x >= initialXPos))
            {
                if ((!negativeY && transform.position.y <= initialYPos) || (negativeY && transform.position.y >= initialYPos))
                {
                    hasFinishedMoving2 = true;
                }
            }

            yield return null;
            
        }
    }

    // move trap out of initial position
    private void ActivateTrap()
    {
        tempPos = transform.position;

        if ((!negativeX && transform.position.x <= finalXPos) || (negativeX && transform.position.x >= finalXPos))
        tempPos.x += (negativeX ? -1 : 1) * 0.1f* moveSpeedX; //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false

        if ((!negativeY && transform.position.y <= finalYPos) || (negativeY && transform.position.y >= finalYPos))
        tempPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY;

        transform.position = tempPos;
    }

    // move trap back to initial position
    private void DeactivateTrap()
    {
        tempPos = transform.position;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if ((!negativeX && transform.position.x >= initialXPos) || (negativeX && transform.position.x <= initialXPos))
        tempPos.x += (negativeX ? 1 : -1) * 0.1f * moveSpeedX;

        if ((!negativeY && transform.position.y >= initialYPos) || (negativeY && transform.position.y <= initialYPos))
        tempPos.y += (negativeY ? 1 : -1) * 0.1f * moveSpeedY;

        transform.position = tempPos;
    }
}
