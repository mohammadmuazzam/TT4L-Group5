using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField]
    Trap[] trapScripts;

    [SerializeField]
    GameObject[] trapTriggers;

    bool trapActivated;
    public float playerMinX, playerMaxX;

    
    void Awake()
    {
        trapActivated = false;
    }

    void Update()
    {
        CheckForTrapTrigger();
    }

    IEnumerator MoveTrapNonStop(Trap trapScript, int mode)
    {
        if (mode == 1) // perma move
            yield return StartCoroutine(trapScript.PermanentMoveTrap());
        else if (mode == 2) // temp move
            yield return StartCoroutine(trapScript.TemporaryMoveTrap());
    }

    void CheckForTrapTrigger()
    {
        // Check if any trap trigger has been triggered
        foreach (GameObject triggerGameObject in trapTriggers)
        {
           
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();
            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // Trigger the trap if the player should jump
                if (Player.shouldJump && triggerGameObject.name == "Trap Trigger")
                {   
                    
                    // Start the coroutine to move the trap
                    StartCoroutine(MoveTrapNonStop(trapScripts[0], 2));
                    Debug.Log("hello");
                }

                // Mark the trap as activated to prevent repeated triggering
                trapTriggerScript.playerIsInTrigger = false; 
                break;
            }
        }
    }

        
}


