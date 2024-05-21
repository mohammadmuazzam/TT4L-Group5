using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Rock : Trap
{
    [SerializeField] private bool isDefaultActive;
    [SerializeField] float finalScaleX, finalScaleY, scaleSpeedX, scaleSpeedY, offsetInitialYPos, offsetInitialXPos;
    private float tempCurrentScaleX, tempCurrentScaleY, initialScaleX, initialScaleY;

    private bool isScaleUpX, isScaleUpY;
    private bool doneScaling, doneScaling2;
    void Awake()
    {
        initialXPos = transform.localPosition.x;
        initialYPos = transform.localPosition.y; 
        

        finalXPos = initialXPos + moveAmountX;
        finalYPos = initialYPos + moveAmountY;
        initialYPos = initialYPos + offsetInitialYPos;
        initialXPos = initialXPos + offsetInitialXPos;
        Debug.Log($"in rock final pos: ({finalXPos}, {finalYPos})");

        //* check if final position is less than initial position
        if (finalXPos < initialXPos) negativeX = true;
        else negativeX = false;

        if (finalYPos < initialYPos) negativeY = true;
        else negativeY = false;

        gameObject.SetActive(isDefaultActive);
        doneScaling2 = false;
        doneScaling = false;

        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;

        if (finalScaleX > transform.localScale.x)
            isScaleUpX = true;
        else
            isScaleUpX = false;
        
        
        if (finalScaleY > transform.localScale.y)
            isScaleUpY = true;
        else
            isScaleUpY = false;    
    }

    public void MoveAndGrowRock()
    {
        try
        {
            gameObject.SetActive(true);

            _ = PositiveScaleRock();
            Debug.Log("Calling TemporaryMoveTrap");
            _ = base.TemporaryMoveTrap();
            Debug.Log("MoveAndGrowRock completed");
        }
        catch (System.Exception)
        {
            return;
        }
        
  
    }

    private async Task PositiveScaleRock()
    {
        try
        {
            while (!doneScaling)
            {

                tempCurrentScaleX = transform.localScale.x;
                tempCurrentScaleX += (isScaleUpX? 1 : -1) * scaleSpeedX * 0.01f;

                tempCurrentScaleY = transform.localScale.y;
                tempCurrentScaleY += (isScaleUpY? 1 : -1) * scaleSpeedY * 0.01f;


                // stop scaling condition
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


    private void NegativeScaleRock()
    {
            tempCurrentScaleX = transform.localScale.x;
            tempCurrentScaleX += (isScaleUpX? -1 : 1) * scaleSpeedX * 0.01f;

            tempCurrentScaleY = transform.localScale.y;
            tempCurrentScaleY += (isScaleUpY? -1 : 1) * scaleSpeedY * 0.01f;

            // stop scaling condition
            if ((isScaleUpX && tempCurrentScaleX <= initialScaleX) || (!isScaleUpX && tempCurrentScaleX >= initialScaleX))
            {
                if ((isScaleUpY && tempCurrentScaleY <= initialScaleX) || (!isScaleUpY && tempCurrentScaleY >= initialScaleY))
                {
                    doneScaling2 = true;
                }
            }

            transform.localScale = new Vector3(tempCurrentScaleX, tempCurrentScaleY, transform.localScale.z);   
    }

    void ActivateRock()
    {
        gameObject.SetActive(true);
    }
    void DeactivateRock()
    {
        gameObject.SetActive(false);
    }
}
