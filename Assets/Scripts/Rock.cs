using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Rock : Trap
{
    [SerializeField] private bool isDefaultActive;
    [SerializeField] float finalScaleX, finalScaleY, scaleSpeedX, scaleSpeedY;
    private float tempCurrentScaleX, tempCurrentScaleY, initialScaleX, initialScaleY;

    private bool isScaleUpX, isScaleUpY;
    private bool doneScaling, doneScaling2;
    void Awake()
    {
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

    public async void MoveAndGrowRock()
    {
        ActivateRock();

        await PositiveScaleRock();
  
    }

    private async Task PositiveScaleRock()
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
