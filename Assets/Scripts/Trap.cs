using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //public static bool hasTrapMoved;
    [SerializeField]
    protected float moveAmountX, moveAmountY, moveSpeedX, moveSpeedY;
    [SerializeField]
    private int waitTime;

    protected float finalXPos, finalYPos, initialXPos, initialYPos;
    protected bool negativeX, negativeY;
    Vector3 tempPos;
    void Awake()
    {
        //! initial and final pos isn't the same as in inspector
        initialXPos = transform.localPosition.x;
        initialYPos = transform.localPosition.y; 
        UnityEngine.Debug.Log($"initial pos: ({initialXPos}, {initialYPos})");

        finalXPos = initialXPos + moveAmountX;
        finalYPos = initialYPos + moveAmountY;
        UnityEngine.Debug.Log($"final pos: ({finalXPos}, {finalYPos})");

        //* check if final position is less than initial position
        if (finalXPos < initialXPos) negativeX = true;
        else negativeX = false;

        if (finalYPos < initialYPos) negativeY = true;
        else negativeY = false;
        
    }

    // move trap
    public async Task PermanentMoveTrap()
    {
        Stopwatch watch = Stopwatch.StartNew();
        bool hasFinishedMoving = false;
        watch.Start();
        while(!hasFinishedMoving)
        {
            ActivateTrap();
            
            // if trap has reached final position, then stop moving and stop coroutine
            if ((!negativeX && transform.localPosition.x >= finalXPos) || (negativeX && transform.localPosition.x <= finalXPos))
            {
                if ((!negativeY && transform.localPosition.y >= finalYPos) || (negativeY && transform.localPosition.y <= finalYPos))
                {
                    hasFinishedMoving = true;
                }
            }
            await Task.Yield();
        }
        watch.Stop();
        print($"elapsed time: {watch.ElapsedMilliseconds}");
    }

    public async Task TemporaryMoveTrap()
    {
        bool hasFinishedMoving1 = false;
        UnityEngine.Debug.Log($"in temp trap, final pos: ({finalXPos}, {finalYPos}), current world pos: {transform.position}, current local pos: {transform.localPosition}");
        
        // trap activating 
        try
        {
            while(!hasFinishedMoving1)
            {
                if (gameObject == null || this == null || UnityEngine.Object.ReferenceEquals(gameObject, null))
                {
                    return;
                }
                else
                {
                    ActivateTrap();

                    // if trap has reached final position, then stop moving and stop coroutine
                    if ((!negativeX && transform.localPosition.x >= finalXPos) || (negativeX && transform.localPosition.x <= finalXPos))
                    {
                        if ((!negativeY && transform.localPosition.y >= finalYPos) || (negativeY && transform.localPosition.y <= finalYPos))
                        {
                            tempPos.x = finalXPos;
                            tempPos.y = finalYPos;
                            transform.localPosition = tempPos;
                            hasFinishedMoving1 = true;
                        }
                    }

                    await Task.Yield();
                }
            }

            // wait
            bool hasFinishedMoving2 = false;
            await Task.Delay(waitTime);
            UnityEngine.Debug.Log("going back to original positin");

            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            // trap going back to original position
            while (!hasFinishedMoving2)
            {
                if (gameObject == null)
                {
                    return;
                }
                DeactivateTrap();

                if ((!negativeX && transform.localPosition.x <= initialXPos) || (negativeX && transform.localPosition.x >= initialXPos))
                {
                    if ((!negativeY && transform.localPosition.y <= initialYPos) || (negativeY && transform.localPosition.y >= initialYPos))
                    {
                        tempPos.x = initialXPos;
                        tempPos.y = initialYPos;
                        transform.localPosition = tempPos;
                        hasFinishedMoving2 = true;
                    }
                }

                await Task.Yield();  
            }
            watch.Stop();
        } 
        catch (Exception ex)
        {
            return;
        }
        
        UnityEngine.Debug.Log("done with TempMoveTrap");
    }

    // move trap out of initial position
    private void ActivateTrap()
    {
        tempPos = transform.localPosition;
        
        if ((!negativeX && transform.localPosition.x < finalXPos) || (negativeX && transform.localPosition.x > finalXPos))
        {
            UnityEngine.Debug.Log("ActivatingTrap in X, tempPos.x before: " + tempPos.x);
            tempPos.x += (negativeX ? -1 : 1) * 0.1f * moveSpeedX * Time.deltaTime; //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
            UnityEngine.Debug.Log("tempPos.x after: " + tempPos.x);
        }
        else
            UnityEngine.Debug.Log("not ActivatingTrap in X");

        //UnityEngine.Debug.Log($"is Y negative: {negativeY}, current Y pos: {transform.localPosition.y}, finalYPos: {finalYPos}");
        if ((!negativeY && transform.localPosition.y < finalYPos) || (negativeY && transform.localPosition.y > finalYPos))
        {
            tempPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY * Time.deltaTime;
        }
        else
            UnityEngine.Debug.Log("not ActivatingTrap in Y");
        transform.localPosition = tempPos;
        
    }

    // move trap back to initial position
    private void DeactivateTrap()
    {
        tempPos = transform.localPosition;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if ((!negativeX && transform.localPosition.x > initialXPos) || (negativeX && transform.localPosition.x < initialXPos))
        tempPos.x += (negativeX ? 1 : -1) * 0.1f * moveSpeedX * Time.deltaTime;


        if ((!negativeY && transform.localPosition.y > initialYPos) || (negativeY && transform.localPosition.y < initialYPos))
        tempPos.y += (negativeY ? 1 : -1) * 0.1f * moveSpeedY * Time.deltaTime;

        transform.localPosition = tempPos;
    }
}