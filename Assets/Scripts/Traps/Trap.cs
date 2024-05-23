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
    [SerializeField] private int waitTime;
    [SerializeField] Vector3[] stopPoints;

    protected float finalXPos, finalYPos, initialXPos, initialYPos;
    protected bool negativeX, negativeY;
    Vector3 tempPos;
    protected virtual void Awake()
    {//!GET COMPONENT OF TRAP
        Trap trap;
        try
        {
            trap = GetComponent<Trap>();
            print($"trap = {trap.gameObject.name}");
        }
        catch (Exception)
        {
            print("no trap script");
        }
        print($"returning from Trap awake, gettype is {GetType()}, typeof(Trap) is {typeof(Trap)}");
        if (GetType() != typeof(Trap))
        {
            print("no trap script");
            return;
        }
        else
            Initialization();
        
    }

    protected void Initialization()
    {
        print("initializing");
        initialXPos = gameObject.transform.localPosition.x;
        initialYPos = gameObject.transform.localPosition.y; 
        UnityEngine.Debug.Log($"Trap - initial pos: ({transform.localPosition.x}, {transform.localPosition.y})");

        finalXPos = initialXPos + moveAmountX;
        finalYPos = initialYPos + moveAmountY;
        UnityEngine.Debug.Log($"Trap - final pos: ({finalXPos}, {finalYPos})");

        //* check if final position is less than initial position
        if (finalXPos < initialXPos) negativeX = true;
        else negativeX = false;

        if (finalYPos < initialYPos) negativeY = true;
        else negativeY = false;
    }

    // move trap
    public async Task PermanentMoveTrap(float stopX, float stopY)
    {
        Stopwatch watch = Stopwatch.StartNew();
        bool hasFinishedMoving = false;
        watch.Start();
        print("Starting PermanentMoveTrap");

        if (stopY < initialXPos) negativeX = true;
        else negativeX = false;

        if (stopY < initialYPos) negativeY = true;
        else negativeY = false;

        while(!hasFinishedMoving)
        {
            print("gameobject active status: " + gameObject.activeSelf);
            ActivateTrap(stopX, stopY);
            
            // if trap has reached final position, then stop moving and stop coroutine
            if ((!negativeX && transform.localPosition.x >= stopX) || (negativeX && transform.localPosition.x <= stopX))
            {
                if ((!negativeY && transform.localPosition.y >= stopY) || (negativeY && transform.localPosition.y <= stopY))
                {
                    hasFinishedMoving = true;
                }
            }
            await Task.Yield();
        }
        watch.Stop();
        print($"elapsed time: {watch.ElapsedMilliseconds}");
    }
    public async Task PermanentMoveTrap()
    {
        await PermanentMoveTrap(finalXPos, finalYPos);
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
        catch (Exception)
        {
            return;
        }
        
    }

    // move trap out of initial position
    private void ActivateTrap(float stopX, float stopY)
    {
        tempPos = transform.localPosition;
        
        if ((!negativeX && transform.localPosition.x < stopX) || (negativeX && transform.localPosition.x > stopX))
        {
            //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
            tempPos.x += (negativeX ? -1 : 1) * 0.1f * moveSpeedX * Time.deltaTime;
            print("ActivatingTrap in X");
        }
        else
            UnityEngine.Debug.Log("not ActivatingTrap in X");

        if ((!negativeY && transform.localPosition.y < stopY) || (negativeY && transform.localPosition.y > stopY))
        {
            tempPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY * Time.deltaTime;
            print("ActivatingTrap in Y");
        }
        else
            UnityEngine.Debug.Log("not ActivatingTrap in Y");
        
        transform.localPosition = tempPos;
        print("After ActivateTrap - position: " + transform.localPosition);
    }
    private void ActivateTrap()
    {
        ActivateTrap(finalXPos, finalYPos);
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