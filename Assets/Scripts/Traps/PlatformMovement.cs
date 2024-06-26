using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float startXPos, endXPos, startYPos, endYPos, moveSpeedX, moveSpeedY;
    [SerializeField] bool moveX, moveY, startToEnd;
    bool negativeX, negativeY;

    public UnityEngine.Vector3 moveAmountPos;
    private UnityEngine.Vector3 tempPos;

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

    void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    private async void Movement()
    {
        bool firstTime = true;
        try
        {
            while(true)
            {
                if (firstTime)
                {
                    firstTime = false;
                }
                // move from start to end
                if (startToEnd)
                {
                    StartToEnd();
                
                    // if trap has reached final position, then stop moving and stop coroutine
                    if ((!negativeX && transform.localPosition.x >= endXPos) || (negativeX && transform.localPosition.x <= endXPos) || !moveX)
                    {
                        if ((!negativeY && transform.localPosition.y >= endYPos) || (negativeY && transform.localPosition.y <= endYPos) || !moveY)
                        {
                            if (moveX)
                                tempPos.x = endXPos;

                            if (moveY)
                                tempPos.y = endYPos;

                            transform.localPosition = tempPos;
                            //print($"{gameObject.name.ToUpper()} reached end position: {tempPos}");
                            
                            startToEnd = false;
                            firstTime = true;
                        }
                    }
                    await Task.Yield();
                }
                // move from end to start   
                else
                {
                    if (firstTime)
                    {
                        firstTime = false;
                    }
                    EndToStart();

                    if ((!negativeX && transform.localPosition.x <= startXPos) || (negativeX && transform.localPosition.x >= startXPos) || !moveX)
                    {
                        if ((!negativeY && transform.localPosition.y <= startYPos) || (negativeY && transform.localPosition.y >= startYPos) || !moveY)
                        {
                            if (moveX)
                            tempPos.x = startXPos;

                            if (moveY)
                            tempPos.y = startYPos;

                            transform.localPosition = tempPos;
                            //print($"{gameObject.name.ToUpper()} reached start position: {tempPos}");
                            
                            startToEnd = true;
                            firstTime = true;
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
        tempPos = transform.localPosition;
        
        if (moveX)
        {
            if ((!negativeX && transform.localPosition.x < endXPos) || (negativeX && transform.localPosition.x > endXPos))
            {
                //* (negativeX ? -1 : 1) returns -1 if negativeX is true and 1 if negativeX is false
                moveAmountPos.x = (negativeX ? -1 : 1) * 0.1f * moveSpeedX * Time.deltaTime;
                tempPos.x += moveAmountPos.x;
                //print("ActivatingTrap in X");
            }
            else
            {
                UnityEngine.Debug.Log("not ActivatingTrap in X");
            }
        }

        if (moveY)
        {
            if ((!negativeY && transform.localPosition.y < endYPos) || (negativeY && transform.localPosition.y > endYPos))
            {
                moveAmountPos.y = (negativeY ? -1 : 1) * 0.1f * moveSpeedY * Time.deltaTime;
                tempPos.y += moveAmountPos.y;
                //print("ActivatingTrap in Y");
            }
            else
            {
                UnityEngine.Debug.Log("not ActivatingTrap in Y");
            }
        }    
        
        transform.localPosition = tempPos;
    }

    private void EndToStart()
    {
        tempPos = transform.localPosition;

        //* (negativeX ? 1 : -1) returns 1 if negativeX is true and -1 if negativeX is false
        if (moveX)
        {
            if ((!negativeX && transform.localPosition.x > startXPos) || (negativeX && transform.localPosition.x < startXPos))
            {
                moveAmountPos.x = (negativeX ? 1 : -1) * 0.1f * moveSpeedX * Time.deltaTime;
                tempPos.x += moveAmountPos.x; 
            }
        }
        
        if (moveY)
        {
            if ((!negativeY && transform.localPosition.y > startYPos) || (negativeY && transform.localPosition.y < startYPos))
            {
                moveAmountPos.y = (negativeY ? 1 : -1) * 0.1f * moveSpeedY * Time.deltaTime;
                tempPos.y += moveAmountPos.y;
            }
        }
        

        transform.localPosition = tempPos;
    }
}
