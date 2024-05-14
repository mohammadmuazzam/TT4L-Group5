using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool trap2Activated, trap3Activated;
    public float playerMinX, playerMaxX;

    void Awake()
    {
        trap2Activated = false;
        trap3Activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTrapTrigger();
    }

    //coroutine to move trap
    IEnumerator MoveTrapNonStop(Trap trapScript, int mode)
    {
        if (mode == 1) // perma move
            yield return StartCoroutine(trapScript.PermanentMoveTrap());
        else if (mode == 2) // temp move
            yield return StartCoroutine(trapScript.TemporaryMoveTrap());
    }

    void CheckForTrapTrigger()
    {
        // check if the trap trigger has been triggered
        foreach (GameObject triggerGameObject in trapTriggers)
        {
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();
            
            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // trigger traps according to trapTrigger
                switch (trapTriggerScript.name)
                {
                    case "Trap Trigger 1":
                    if (Player.shouldJump)
                    {   
                        StartCoroutine(trapScripts[0].PermanentMoveTrap());
                    }
                    break;

                    case "Trap Trigger 2":
                    if (!trap2Activated)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[1], 2));
                        trap2Activated = true;
                    }         
                    break;

                    case "Trap Trigger 3":
                    if (!trap3Activated)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[2], 2));
                        trap3Activated = true;
                    }
                    break;

                }
            }
        }
    }
}

