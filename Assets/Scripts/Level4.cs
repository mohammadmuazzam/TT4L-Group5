using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private Lightning lightningScripts;
    [SerializeField] private Rock rockScript;

    [SerializeField]private GameObject[] trapTriggers;

    private bool hasTriggered1, hasTriggered2;
    void Awake()
    {
        hasTriggered1 = false;
        hasTriggered2 = false;
    }

    // Update is called once per frame
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
                        StartCoroutine(trapScripts[0].PermanentMoveTrap());
                    }
                    break;
                }

            }
        }
    }

    IEnumerator StartTraps(Lightning lightningScript)
    {

        yield return StartCoroutine(lightningScript.SpawnLightning());
    }
}
