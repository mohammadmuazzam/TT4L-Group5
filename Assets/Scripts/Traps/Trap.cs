using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //? movement data
    [SerializeField] protected float moveAmountX, moveAmountY, moveSpeedX, moveSpeedY;
    [SerializeField] private int waitTime;
    protected float finalXPos, finalYPos, initialXPos, initialYPos;
    protected bool negativeX, negativeY;
    Vector3 tempPos;

    //* scale data
    [SerializeField] float finalScaleX, finalScaleY, scaleSpeedX, scaleSpeedY, offsetInitialYPos, offsetInitialXPos;
    private float tempCurrentScaleX, tempCurrentScaleY, initialScaleX, initialScaleY;
    private bool doneScaling, doneScaling2;
    protected bool isScaleUpX, isScaleUpY;

    //* sfx
    [SerializeField] AudioClip trapOut, trapIn;
    
    [Range(0, 1)][SerializeField] float volumeOut, volumeIn;
    
    protected virtual void Awake()
    {
        Initialization();   
    }
    protected void Initialization()
    {
        //? initialize movement
        initialXPos = gameObject.transform.localPosition.x;
        initialYPos = gameObject.transform.localPosition.y; 
        //UnityEngine.Debug.Log($"Trap - initial pos: ({transform.localPosition.x}, {transform.localPosition.y})");

        finalXPos = initialXPos + moveAmountX;
        finalYPos = initialYPos + moveAmountY;
        //UnityEngine.Debug.Log($"Trap - final pos: ({finalXPos}, {finalYPos})");
        if (finalXPos < initialXPos) negativeX = true;
        else negativeX = false;

        if (finalYPos < initialYPos) negativeY = true;
        else negativeY = false;

        //? initialize scale
        if (finalScaleX > transform.localScale.x)
            isScaleUpX = true;
        else
            isScaleUpX = false;
        
        if (finalScaleY > transform.localScale.y)
            isScaleUpY = true;
        else
            isScaleUpY = false;   

        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
    }

    //* move trap await Task.Delay(timer)
    public async Task PermanentMoveTrap(float startX, float startY, float stopX, float stopY)
    {
        try
        {
            Stopwatch watch = Stopwatch.StartNew();
            bool hasFinishedMoving = false;
            watch.Start();
            //print("Starting PermanentMoveTrap");

            if (stopX < startX) negativeX = true;
            else negativeX = false;

            if (stopY < startY) negativeY = true;
            else negativeY = false;

            //* play sound
            if (trapOut != null && Player.isPlayerAlive)
                SoundFxManager.Instance.PlaySoundFxClip(trapOut, transform, volumeOut);


            while(!hasFinishedMoving)
            {
                ActivateTrap(stopX, stopY);
                
                // if trap has reached final position
                if ((!negativeX && transform.localPosition.x >= stopX) || (negativeX && transform.localPosition.x <= stopX))
                {
                    if ((!negativeY && transform.localPosition.y >= stopY) || (negativeY && transform.localPosition.y <= stopY))
                    {
                        if (startX != stopX)
                            tempPos.x = stopX;

                        if (startY != stopY)
                            tempPos.y = stopY;
                        transform.localPosition = tempPos;
                        hasFinishedMoving = true;
                    }
                }
                await Task.Yield();
            }
            watch.Stop();
            //print($"elapsed time: {watch.ElapsedMilliseconds}");
        }
        catch(Exception)
        {
            return;
        }
    }
    public async Task PermanentMoveTrap()
    {
        await PermanentMoveTrap(initialXPos, initialYPos, finalXPos, finalYPos);
    }
    public async Task TemporaryMoveTrap(float startX, float startY, float stopX, float stopY)
    {
        bool hasFinishedMoving1 = false;
        //UnityEngine.Debug.Log($"in temp trap, final pos: ({finalXPos}, {finalYPos}), current world pos: {transform.position}, current local pos: {transform.localPosition}");
        
        if (stopX < startX) negativeX = true;
        else negativeX = false;

        if (stopY < startY) negativeY = true;
        else negativeY = false;

        //* play out sound
        if (trapOut != null && Player.isPlayerAlive)
            SoundFxManager.Instance.PlaySoundFxClip(trapOut, transform, volumeOut);


        // trap activating 
        try
        {
            while(!hasFinishedMoving1)
            {
                ActivateTrap(stopX, stopY);
                
                // if trap has reached final position, then stop moving and stop coroutine
                if ((!negativeX && transform.localPosition.x >= stopX) || (negativeX && transform.localPosition.x <= stopX))
                {
                    if ((!negativeY && transform.localPosition.y >= stopY) || (negativeY && transform.localPosition.y <= stopY))
                    {
                        if (startX != stopX)
                        {
                            tempPos.x = stopX;
                        }
                            

                        if (startY != stopY)
                        {
                            tempPos.y = stopY;
                        }
                        //print($"temp move trap activate final pos: {transform.localPosition}");
                        transform.localPosition = tempPos;
                        hasFinishedMoving1 = true;
                    }
                }
                await Task.Yield();
            }

            // wait
            bool hasFinishedMoving2 = false;
            await Task.Delay(waitTime);

            //* play in sound
            if (trapIn != null && Player.isPlayerAlive)
            SoundFxManager.Instance.PlaySoundFxClip(trapIn, transform, volumeIn);

            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            // trap going back to original position
            while (!hasFinishedMoving2)
            {
                DeactivateTrap(startX, startY);

                if ((!negativeX && transform.localPosition.x <= startX) || (negativeX && transform.localPosition.x >= startX))
                {
                    if ((!negativeY && transform.localPosition.y <= startY) || (negativeY && transform.localPosition.y >= startY))
                    {
                        if (startX != stopX)
                            tempPos.x = startX;

                        if (startY != stopY)
                            tempPos.y = startY;
                        transform.localPosition = tempPos;
                        //print($"temp move trap deactivate final pos: {transform.localPosition}");
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
    public async Task TemporaryMoveTrap()
    {
        await TemporaryMoveTrap(initialXPos, initialYPos, finalXPos, finalYPos);
    }
    
    //* move trap out of initial position
    private void ActivateTrap(float stopX, float stopY)
    {
        try
        {
            tempPos = transform.localPosition;
            
            if ((!negativeX && transform.localPosition.x < stopX) || (negativeX && transform.localPosition.x > stopX))
            {
                //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
                tempPos.x += (negativeX ? -1 : 1) * 0.1f * moveSpeedX * Time.deltaTime;
                //print("ActivatingTrap in X");
            }
            else
            {
                //UnityEngine.Debug.Log("not ActivatingTrap in X");
            }
                

            if ((!negativeY && transform.localPosition.y < stopY) || (negativeY && transform.localPosition.y > stopY))
            {
                tempPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY * Time.deltaTime;
                //print("ActivatingTrap in Y");
            }
            else
            {
                //UnityEngine.Debug.Log("not ActivatingTrap in Y");
            }
                
            
            transform.localPosition = tempPos;
            //print("After ActivateTrap - position: " + transform.localPosition);
        }
        catch (Exception)
        {
            return;
        }
    }
    
    //* move trap back to initial position
    private void DeactivateTrap(float startX, float startY)
    {
        tempPos = transform.localPosition;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if ((!negativeX && transform.localPosition.x > startX) || (negativeX && transform.localPosition.x < startX))
        tempPos.x += (negativeX ? 1 : -1) * 0.1f * moveSpeedX * Time.deltaTime;


        if ((!negativeY && transform.localPosition.y > startY) || (negativeY && transform.localPosition.y < startY))
        tempPos.y += (negativeY ? 1 : -1) * 0.1f * moveSpeedY * Time.deltaTime;

        transform.localPosition = tempPos;
    }
    
    //*Scale
    protected async Task PositiveScaleRock()
    {
        try
        {
            doneScaling = false;
            while (!doneScaling)
            {
                //* scale rock
                tempCurrentScaleX = transform.localScale.x;
                tempCurrentScaleX += (isScaleUpX? 1 : -1) * scaleSpeedX * Time.deltaTime;

                tempCurrentScaleY = transform.localScale.y;
                tempCurrentScaleY += (isScaleUpY? 1 : -1) * scaleSpeedY * Time.deltaTime;


                //* stop scaling condition
                if ((isScaleUpX && tempCurrentScaleX >= finalScaleX) || (!isScaleUpX && tempCurrentScaleX <= finalScaleX))
                {
                    if ((isScaleUpY && tempCurrentScaleY >= finalScaleY) || (!isScaleUpY && tempCurrentScaleY <= finalScaleY))
                    {
                        doneScaling = true;
                    }
                }
                transform.localScale = new Vector3(tempCurrentScaleX, tempCurrentScaleY, transform.localScale.z);   
                await Task.Yield();
            }
        }
        catch (System.Exception)
        {
            return;
        }
        
        
    }
    protected async void NegativeScaleRock()
    {
        try
        {
            doneScaling2 = false;
            while (!doneScaling2)
            {
                tempCurrentScaleX = transform.localScale.x;
                tempCurrentScaleX += (isScaleUpX? -1 : 1) * scaleSpeedX * Time.deltaTime;

                tempCurrentScaleY = transform.localScale.y;
                tempCurrentScaleY += (isScaleUpY? -1 : 1) * scaleSpeedY * Time.deltaTime;

                // stop scaling condition
                if ((isScaleUpX && tempCurrentScaleX <= initialScaleX) || (!isScaleUpX && tempCurrentScaleX >= initialScaleX))
                {
                    if ((isScaleUpY && tempCurrentScaleY <= initialScaleX) || (!isScaleUpY && tempCurrentScaleY >= initialScaleY))
                    {
                        doneScaling2 = true;
                    }
                }

                transform.localScale = new Vector3(tempCurrentScaleX, tempCurrentScaleY, transform.localScale.z);   
                await Task.Yield();
            }
        }
        catch (Exception)
        {
            return;
        }
            
    }


}