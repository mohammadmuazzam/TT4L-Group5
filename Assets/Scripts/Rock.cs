using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Trap
{
    [SerializeField] private bool isDefaultActive;
    [SerializeField] float finalScaleX, finalScaleY, scaleSpeedX, scaleSpeedY;
    private float tempCurrentScaleX, tempCurrentScaleY, initialScaleX, initialScaleY;

    private bool isScaleUpX, isScaleUpY;
    private bool doneGrowing2, doneGrowing;
    void Awake()
    {
        gameObject.SetActive(isDefaultActive);
        doneGrowing2 = false;
        doneGrowing = false;

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

    public IEnumerator MoveAndGrowRock()
    {
        ActivateRock();
    
        // move and grow
        while (!doneGrowing2)
        {
            // scale
            if (!doneGrowing)
            {
                PositiveScaleRock();
                if ((isScaleUpX && tempCurrentScaleX >= finalScaleX) || (!isScaleUpX && tempCurrentScaleX <= finalScaleX))
                {
                    if ((isScaleUpY && tempCurrentScaleY >= finalScaleY) || (!isScaleUpY && tempCurrentScaleY <= finalScaleY))
                    {
                        doneGrowing = true;
                    }
                }
                yield return null;
            }
            yield return new WaitForSeconds(1);
            if (!doneGrowing2)
            {
                NegativeScaleRock();
                if ((isScaleUpX && tempCurrentScaleX <= initialScaleX) || (!isScaleUpX && tempCurrentScaleX >= initialScaleX))
                {
                    if ((isScaleUpY && tempCurrentScaleY <= initialScaleX) || (!isScaleUpY && tempCurrentScaleY >= initialScaleY))
                    {
                        doneGrowing2 = true;
                    }
                }
                yield return null;
            }
            
        }
        
    }

    private void PositiveScaleRock()
    {
            tempCurrentScaleX = transform.localScale.x;
            tempCurrentScaleX += (isScaleUpX? 1 : -1) * scaleSpeedX * 0.01f;

            tempCurrentScaleY = transform.localScale.y;
            tempCurrentScaleY += (isScaleUpY? 1 : -1) * scaleSpeedY * 0.01f;

            transform.localScale = new Vector3(tempCurrentScaleX, tempCurrentScaleY, transform.localScale.z);   
    }

    private void NegativeScaleRock()
    {
            tempCurrentScaleX = transform.localScale.x;
            tempCurrentScaleX += (isScaleUpX? -1 : 1) * scaleSpeedX * 0.01f;

            tempCurrentScaleY = transform.localScale.y;
            tempCurrentScaleY += (isScaleUpY? -1 : 1) * scaleSpeedY * 0.01f;

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
