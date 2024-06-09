using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    [SerializeField] private GameObject[] trapTriggers;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject catOnlyObject;
    [SerializeField] private bool[] hasTriggered;
    [SerializeField] private float speed;

    private Boss catBossScript;
    private bool zeroCondition = false;

    void Awake()
    {
        catBossScript = catOnlyObject.GetComponent<Boss>();
        for (int i = 0; i < hasTriggered.Length; i++)
        {
            hasTriggered[i] = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckForTrapTrigger();

        CheckAttemptAndBossHealth();
    }

    void CheckForTrapTrigger()
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
                }
            }
        }
    }

    void CheckAttemptAndBossHealth()
    {
        //* traps leading to first kill
        //print("attempt: " + GameManager.attempts + ", boss health: " + catBossScript.bossHealth);

        if (catBossScript.bossHealth == 3 && !zeroCondition)
        {
            zeroCondition = true;
            catBossScript.bossHealth = 3;
            PlayerSlowMoAfterBossKill();
            
        }
    }

    private async void PlayerSlowMoAfterBossKill()
    {
        await Task.Delay(300);

        Player.playerBody.gravityScale = 0.00f;
        Player.canPlayerMove = false;
        await TeleportBossAndMoveCamera();

        await Task.Delay(4000);

        Player.playerBody.gravityScale = 10f;
        Player.canPlayerMove = true;
    }

    private async Task TeleportBossAndMoveCamera()
    {
        //* initialization
        CameraFollow cameraScript = mainCamera.GetComponent<CameraFollow>();
        Vector3 tempPos = mainCamera.transform.position;
        cameraScript.maxX = 20;
        cameraScript.playerDependant = false;
        
        tempPos.x = catOnlyObject.transform.position.x;
        await ShrinkAndGrowCat(0);

        //* move camera
        while (mainCamera.transform.position.x < 20)
        {
            //print("moving camera");
            tempPos.x += speed*Time.deltaTime;
            mainCamera.transform.position = tempPos;
            await Task.Yield();
        }

        cameraScript.playerDependant = true;
    }

    private async Task ShrinkAndGrowCat(int i)
    {
        Vector3 initialScale = catOnlyObject.transform.localScale;

        if (i == 0)
        {
            while (initialScale.x > 0)
            {
                initialScale = catOnlyObject.transform.localScale;
                initialScale = new Vector3 ((float) (initialScale.x - 0.01*Time.deltaTime), (float) (initialScale.y - 0.01*Time.deltaTime), (float) (initialScale.z - 0.01*Time.deltaTime));
                await Task.Yield();
            }
            if (initialScale.x <= 0)
            {
                initialScale = new Vector3 (0,0,0);
            }

            catOnlyObject.transform.localScale = initialScale;
        }
        else if (i == 1)
        {
            
        }
        

    }
}
