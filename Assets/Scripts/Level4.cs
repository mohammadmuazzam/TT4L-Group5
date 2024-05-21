using System.Collections;
using System.Collections.Generic;
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

    private bool hasTriggered1, hasTriggered2, hasCloudMoved;
    

    void Awake()
    {
        hasTriggered1 = false;
        hasTriggered2 = false;
        hasCloudMoved = false;
    }


    void Update()
    {
        CheckForTrapTrigger();
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
                        //StartCoroutine(CloudMovementCheck(trapScripts[0].PermanentMoveTrap()));
                        if (!hasCloudMoved)
                        {
                            hasCloudMoved = true;
                            StartCoroutine(RunCoroutineWithStatus(trapScripts[0].PermanentMoveTrap(), cloudMovementCoroutineStatus));
                        }
                        Debug.Log("cloud status: " + cloudMovementCoroutineStatus.IsCompleted);

                        if (cloudMovementCoroutineStatus.IsCompleted)
                        {
                            rockScript.MoveAndGrowRock();
                            hasTriggered2 = true;
                        }
                            
                    }
                    break;

                    case "Trap Trigger 3":
                    if (Player.shouldJump)
                    {
                        StartCoroutine(trapScripts[1].TemporaryMoveTrap());
                    }
                    break;
                }

            }
        }
    }

    IEnumerator RunCoroutineWithStatus(IEnumerator coroutine, CoroutineStatus status)
    {
        status.IsCompleted = false;
        yield return StartCoroutine(coroutine);
        status.IsCompleted = true;
    }
}