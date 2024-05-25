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

    [SerializeField] private GameObject[] trapTriggers;

    private bool hasTriggered1, hasTriggered2, hasTriggered3, hasTriggered4, hasTriggered5, 
    hasTriggered6, hasTriggered7 , pitTrigger, tempSpike = false;

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
                        _ = rockScript.MoveAndGrowRockLevel3();
                        hasTriggered2 = true;
                        
                    }

                    //? move trap if player jump over death hole
                    if (Player.shouldJump && !tempSpike)
                    {
                        _ = trapScripts[0].TemporaryMoveTrap();
                        tempSpike = true;
                    }
                    break;

                    case "Trap Trigger 3":
                    if (!hasTriggered3)
                    {
                        _ = trapScripts[2].PermanentMoveTrap();
                        hasTriggered3 = true;
                    }
                    break;

                    case "Trap Trigger 4":
                    if (hasTriggered3 && !hasTriggered4)
                    {
                        _ = trapScripts[1].PermanentMoveTrap();
                        hasTriggered4 = true;
                    }
                    break;

                    //? heavy metal object
                    case "Trap Trigger 5":
                    if (!hasTriggered5)
                    {
                        _ = trapScripts[3].TemporaryMoveTrap();
                        hasTriggered5 = true;
                    }
                    break;

                    //? if player tries to run from heavy metal object
                    case "Trap Trigger 6":
                    if (!hasTriggered6 && hasTriggered5)
                    {
                        _ = trapScripts[4].PermanentMoveTrap();
                        hasTriggered6 = true;
                    }
                    break;

                    //? safe pit
                    case "Pit Trigger":
                    if (!pitTrigger)
                    {
                        _ = trapScripts[5].PermanentMoveTrap();
                        pitTrigger = true;
                    }
                    break;

                    //? gate trap
                    case "Trap Trigger 7":
                    if (!hasTriggered7)
                    {
                        _ = trapScripts[6].PermanentMoveTrap();
                        hasTriggered7 = true;
                    }
                    break;
                }
            }
        }
    }
}