using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        Stopwatch watch = Stopwatch.StartNew();
        bool hasFinishedMoving = false;
        watch.Start();
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
        watch.Stop();
        print($"elapsed time: {watch.ElapsedMilliseconds}");
    }

    public IEnumerator TemporaryMoveTrap()
    {
        bool hasFinishedMoving1 = false;

        // trap activating
        while(!hasFinishedMoving1)
        {
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

        // wait
        bool hasFinishedMoving2 = false;
        yield return new WaitForSeconds(waitTime);

        // trap going back to original position
        while (!hasFinishedMoving2)
        {
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

        if ((!negativeX && transform.position.x < finalXPos) || (negativeX && transform.position.x > finalXPos))
        tempPos.x += (negativeX ? -1 : 1) * 0.1f * moveSpeedX; //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
        else
        tempPos.x = finalXPos;

        if ((!negativeY && transform.position.y < finalYPos) || (negativeY && transform.position.y > finalYPos))
        tempPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY;
        else
        tempPos.y = finalYPos;

        transform.position = tempPos;
    }

    // move trap back to initial position
    private void DeactivateTrap()
    {
        tempPos = transform.position;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if ((!negativeX && transform.position.x > initialXPos) || (negativeX && transform.position.x < initialXPos))
        tempPos.x += (negativeX ? 1 : -1) * 0.1f * moveSpeedX;
        else
        tempPos.x = initialXPos;


        if ((!negativeY && transform.position.y > initialYPos) || (negativeY && transform.position.y < initialYPos))
        tempPos.y += (negativeY ? 1 : -1) * 0.1f * moveSpeedY;
        else
        tempPos.y = initialYPos;

        transform.position = tempPos;
    }
}