using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;
    [SerializeField] private Lightning lightningScripts;
    
    [SerializeField]
    private GameObject[] trapTriggers;
    void Start()
    {
        
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
                    break;
                }

            }
        }
    }
}
