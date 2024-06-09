using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private GameObject[] trapTriggers;
    
    private bool closeTrapTriggered1, platformMoved;

    void Awake()
    {
        closeTrapTriggered1 = false;
        platformMoved = false;
    }

    // Update is called once per frame
    void LateUpdate()
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
                    case "Close Trap Trigger 1":
                    if (trapTriggerScript.movingPlatformIsInTrigger && !closeTrapTriggered1)
                    {
                        _ = trapScripts[0].TemporaryMoveTrap();
                        closeTrapTriggered1 = true;
                    }
                    break;

                    case "Platform Trigger":
                    if (!platformMoved)
                    {
                        _= trapScripts[1].PermanentMoveTrap();
                        platformMoved = true;
                    }
                    break;
                }
            }
        }
    }

}
