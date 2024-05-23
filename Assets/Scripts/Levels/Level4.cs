using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public class CoroutineStatus
    {
        public bool IsCompleted;   
    }
    public CoroutineStatus cloudMovementCoroutineStatus = new CoroutineStatus();

    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private Lightning lightningScripts;
    [SerializeField] private Rock rockScript;

    [SerializeField]private GameObject[] trapTriggers;

    private bool hasTriggered1, hasTriggered2, hasCloudMoved, tempSpike;
    

    void Awake()
    {
        hasTriggered1 = false;
        hasTriggered2 = false;
        hasCloudMoved = false;
        tempSpike = false;
    }


    void LateUpdate()
    {
        CheckForTrapTrigger();
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
                    case "Trap Trigger 1":
                    if (!hasTriggered1)
                    {
                        hasTriggered1 = true;
                        StartCoroutine(lightningScripts.SpawnLightning()); 
                    }
                    break;

                    case "Trap Trigger 2":
                    if (!hasTriggered2)
                    {
                        if (!hasCloudMoved)
                        {
                            hasCloudMoved = true;
                            await rockScript.PermanentMoveTrap();
                            _ = rockScript.MoveAndGrowRock();
                            hasTriggered2 = true;
                            
                        }
                    }
                    if (Player.shouldJump && !tempSpike)
                    {
                        _ = trapScripts[1].TemporaryMoveTrap();
                        tempSpike = true;
                    }
                    break;

                    case "Trap Trigger 3":
                    if (Player.shouldJump)
                    {
                        //StartCoroutine(trapScripts[1].TemporaryMoveTrap());
                    }
                    break;
                }

            }
        }
    }
}