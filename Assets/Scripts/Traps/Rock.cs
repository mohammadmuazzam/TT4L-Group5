using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Rock : Trap
{
    [SerializeField] private bool isDefaultActive;
    [SerializeField] float finalScaleX, finalScaleY, scaleSpeedX, scaleSpeedY, offsetInitialYPos, offsetInitialXPos;
    
    private float tempCurrentScaleX, tempCurrentScaleY, initialScaleX, initialScaleY;

    private bool isScaleUpX, isScaleUpY;
    private bool doneScaling, doneScaling2;
    protected override void Awake()
    {
        print("intializing in rock");
        Initialization();
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

    public async Task MoveAndGrowRockLevel3()
    {
        try
        {
            gameObject.SetActive(true);
            //! GAME OBJECT INS'T ACTUALLY ACTIVE

            await PermanentMoveTrap();
            print("After PermanentMoveTrap");
            _ = PositiveScaleRock();
            _ = TemporaryMoveTrap(initialXPos, initialYPos+10, finalXPos, finalYPos);
            //await PermanentMoveTrap(initialXPos, initialYPos+10);
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
