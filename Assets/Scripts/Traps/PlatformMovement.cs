using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float startXPos, endXPos, startYPos, endYPos, moveSpeedX, moveSpeedY;
    [SerializeField] bool moveX, moveY, startToEnd;
    bool negativeX, negativeY;

    public UnityEngine.Vector3 moveAmountPos;

    // Start is called before the first frame update
    void Awake()
    {
        if (endXPos < startXPos) negativeX = true;
        else negativeX = false;

        if (endYPos < startYPos) negativeY = true;
        else negativeY = false;
    }   

    // Update is called once per frame
    void Start()
    {
        Movement();
    }

    private async void Movement()
    {
        try
        {
            while(true)
            {
                // move from start to end
                if (startToEnd)
                {
                    StartToEnd();
                    print("start to end");
                
                    // if trap has reached final position, then stop moving and stop coroutine
                    if ((!negativeX && transform.localPosition.x >= endXPos) || (negativeX && transform.localPosition.x <= endXPos) || !moveX)
                    {
                        if ((!negativeY && transform.localPosition.y >= endYPos) || (negativeY && transform.localPosition.y <= endYPos) || !moveY)
                        {
                            print("DONE - start to end");
                            if (moveX)
                            moveAmountPos.x = endXPos;

                            if (moveY)
                            moveAmountPos.y = endYPos;

                            transform.localPosition = moveAmountPos;
                            startToEnd = false;
                        }
                    }
                    await Task.Yield();
                }

                // move from end to start   
                else
                {
                    EndToStart();
                    print("end to start");

                    if ((!negativeX && transform.localPosition.x <= startXPos) || (negativeX && transform.localPosition.x >= startXPos) || !moveX)
                    {
                        if ((!negativeY && transform.localPosition.y <= startYPos) || (negativeY && transform.localPosition.y >= startYPos) || !moveY)
                        {
                            if (moveX)
                            moveAmountPos.x = startXPos;

                            if (moveY)
                            moveAmountPos.y = startYPos;

                            transform.localPosition = moveAmountPos;
                            startToEnd = true;
                        }
                    }

                    await Task.Yield();  
                }
                
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    private void StartToEnd()
    {
        moveAmountPos = transform.localPosition;
        
        if (moveX)
        {
            if ((!negativeX && transform.localPosition.x <= endXPos) || (negativeX && transform.localPosition.x >= endXPos))
            {
                //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
                moveAmountPos.x += (negativeX ? -1 : 1) * 0.1f * moveSpeedX * Time.deltaTime;
                //print("ActivatingTrap in X");
            }
            else
            {
                UnityEngine.Debug.Log("not ActivatingTrap in X");
            }
        }

        if (moveY)
        {
            if ((!negativeY && transform.localPosition.y <= endYPos) || (negativeY && transform.localPosition.y >= endYPos))
            {
                moveAmountPos.y += (negativeY ? -1 : 1) * 0.1f * moveSpeedY * Time.deltaTime;
                //print("ActivatingTrap in Y");
            }
            else
            {
                UnityEngine.Debug.Log("not ActivatingTrap in Y");
            }
        }    
        
        transform.localPosition = moveAmountPos;
    }

    private void EndToStart()
    {
        moveAmountPos = transform.localPosition;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if (moveX)
        {
            if ((!negativeX && transform.localPosition.x >= startXPos) || (negativeX && transform.localPosition.x <= startXPos))
            moveAmountPos.x += (negativeX ? 1 : -1) * 0.1f * moveSpeedX * Time.deltaTime;
        }
        
        if (moveY)
        {
            if ((!negativeY && transform.localPosition.y >= startYPos) || (negativeY && transform.localPosition.y <= startYPos))
            moveAmountPos.y += (negativeY ? 1 : -1) * 0.1f * moveSpeedY * Time.deltaTime;
        }
        

        transform.localPosition = moveAmountPos;
    }
}
