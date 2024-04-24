using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomTrap
{
    public static void TestMethod(this Trap trap)
    {
        Debug.Log($"It is working {trap}");
    }
}

public class Level1 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;
    
    [SerializeField]
    private GameObject[] trapTriggers;

    // Update is called once per frame
    void Update()
    {
      CheckForTrapTrigger();  
    }

    //coroutine to move trap
    IEnumerator MoveTrapNonStop(Trap trapScript)
    {
        yield return StartCoroutine(trapScript.PermanentMoveTrap());
    }

    void CheckForTrapTrigger()
    {
        // check if the trap trigger has been triggered
            foreach (GameObject triggerGameObject in trapTriggers)
            {
                TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();

                if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
                {   
                    Debug.Log($"{triggerGameObject.name} has been triggered. player is in trigger: {trapTriggerScript.playerIsInTrigger}");

                    // trigger traps according to trapTrigger
                    switch (trapTriggerScript.name)
                    {
                        case "Trap Trigger 1":
                        if (!Player.isOnGround)
                        {   
                            StartCoroutine(MoveTrapNonStop(trapScripts[0]));
                            Debug.Log($"trap is now moving"); 
                        }
                        break;

                        case "Trap Trigger 2":
                        break;
                    }
                }
            }
    }
}

