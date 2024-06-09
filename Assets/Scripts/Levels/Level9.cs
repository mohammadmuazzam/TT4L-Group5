using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    [SerializeField] private GameObject[] trapTriggers;
    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject catOnlyObject, playerObject;
    [SerializeField] private bool[] hasTriggered;
    [SerializeField] private float speed;

    private Boss catBossScript;
    private Player playerScript;
    private bool fallingPlatformActivated;
    private bool zeroCondition = false;
    Vector3 defaultCatScale;

    void Awake()
    {
        fallingPlatformActivated = false;
        //* initialization
        catBossScript = catOnlyObject.GetComponent<Boss>();
        defaultCatScale = catOnlyObject.transform.localScale;
        playerScript = playerObject.GetComponent<Player>();

        for (int i = 0; i < hasTriggered.Length; i++)
        {
            hasTriggered[i] = false;
        }
        print("zero condition: " + zeroCondition);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckForTrapTrigger();

        CheckAttemptAndBossHealth();
    }

    async void CheckForTrapTrigger()
    {
        // check if any trap trigger has been triggered
        foreach (GameObject triggerGameObject in trapTriggers)
        {
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();

            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // trigger traps according to trapTrigger
                switch (trapTriggerScript.name)
                {
                    case "Cat Trigger 1":
                    if (!hasTriggered[0])
                    {
                        catBossScript.ShootNormalBullets();
                        hasTriggered[0] = true;
                    }
                    break;

                    case "Floor Fall Trigger":
                    {
                        if (!fallingPlatformActivated)
                        {
                            await Task.Delay(500);
                            _ = trapScripts[0].PermanentMoveTrap();
                        }
                    }
                    break;
                }
            }
        }
    }

    async void CheckAttemptAndBossHealth()
    {
        //* traps leading to first kill
        //print("attempt: " + GameManager.attempts + ", boss health: " + catBossScript.bossHealth);

        if (catBossScript.bossHealth == 3 && !zeroCondition)
        {
            zeroCondition = true;
            catBossScript.bossHealth = 3;

            //* boss teleport animation
            await PlayerSlowMoAfterBossKill(47.9f, 57.91f, new Vector3 (70.2f, 3, 0));

            //* boss shoots randomly
            BossShootAtRandomTime();
        }
    }

    private async Task PlayerSlowMoAfterBossKill(float cameraMaxX, float playerMaxX, Vector3 bosFinalPos)
    {
        try
        {
            await Task.Delay(300);

            Player.playerBody.gravityScale = 0.1f;
            Player.playerBody.velocity = new Vector2 (0, 0.1f);
            Player.canPlayerMove = false;

            await TeleportBossAndMoveCamera(cameraMaxX, playerMaxX, bosFinalPos);

            await Task.Delay(300);

            Player.playerBody.gravityScale = 10f;
            Player.canPlayerMove = true;
        }
        catch (System.Exception)
        {
            return;
        }
        
    }

    private async Task TeleportBossAndMoveCamera(float cameraMaxX, float playerMaxX, Vector3 bosFinalPos)
    {
        //* initialization
        CameraFollow cameraScript = mainCamera.GetComponent<CameraFollow>();
        Vector3 tempPos = mainCamera.transform.position;
        cameraScript.maxX = cameraMaxX;
        playerScript.maxX = playerMaxX;
        cameraScript.playerDependant = false;
        
        //* move camera to cat and shrink cat
        tempPos.x = catOnlyObject.transform.position.x;
        await ShrinkAndGrowCat(0);

        //* move camera
        bool doneMoving = false;
        while (!doneMoving)
        {
            //print("moving camera");
            tempPos.x += speed*Time.deltaTime;
            
            if (mainCamera.transform.position.x >= cameraScript.maxX)
            {
                tempPos.x = cameraScript.maxX;
                doneMoving = true;
            }
            mainCamera.transform.position = tempPos;
            await Task.Yield();
        }
        
        //* move and grow cat
        catOnlyObject.transform.parent.transform.position = bosFinalPos;
        await ShrinkAndGrowCat(1);

        cameraScript.playerDependant = true;
    }

    private async Task ShrinkAndGrowCat(int i)
    {
        //* shrink
        if (i == 0)
        {
            bool doneShrinking = false;
            try
            {
                while (!doneShrinking)
                {
                    float tempScale = catOnlyObject.transform.localScale.y;
                    tempScale -= 2 * 0.1f * Time.deltaTime;
                    if (tempScale <= 0)
                    {
                        tempScale = 0;
                        doneShrinking = true;
                    }

                    catOnlyObject.transform.localScale = new Vector3 (-1*tempScale, tempScale, tempScale);
                    await Task.Yield();
                }
            }
            catch (System.Exception)
            {
                return;
            }
            
        }
        //* grow
        else if (i == 1)
        {
            bool doneShrinking = false;
            
            try
            {
               while (!doneShrinking)
                {
                    
                    float tempScale = catOnlyObject.transform.localScale.y;
                    tempScale += 2 * 0.1f * Time.deltaTime;
                    if (tempScale >= defaultCatScale.y)
                    {
                        tempScale = defaultCatScale.y;
                        doneShrinking = true;
                    }

                    catOnlyObject.transform.localScale = new Vector3 (-1*tempScale, tempScale, tempScale);
                    await Task.Yield();
                } 
            }
            catch (System.Exception)
            {
                return;
            }
            
        }
        else
        {
            print("Error: Invalid mode.");
        }
        
        await Task.Delay(500);

    }

    private async void BossShootAtRandomTime()
    {
        while (catBossScript.bossHealth == 3)
        {
            //* random time to shoot
            int randomTime = Random.Range (700, 2200);
            await Task.Delay (randomTime);

            catBossScript.ShootNormalBullets();
            await Task.Yield();

        }
    }
}
