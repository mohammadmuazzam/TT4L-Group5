using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private bool trap1Activated, trap2Activated, trap3Activated;
    public float playerMinX, playerMaxX;

    void Awake()
    {
        trap1Activated = false;
        trap2Activated = false;
        trap3Activated = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckForTrapTrigger();
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
                    if (!trap1Activated && Player.shouldJump)
                    {
                        _ = trapScripts[0].PermanentMoveTrap();
                        trap1Activated = true;
                    }
                    break;

                    case "Trap Trigger 2":
                    if (!trap2Activated)
                    {
                        _ = trapScripts[1].TemporaryMoveTrap();
                        trap2Activated = true;
                    }         
                    break;

                    case "Trap Trigger 3":
                    if (!trap3Activated)
                    {
                        _ = trapScripts[2].TemporaryMoveTrap();
                        trap3Activated = true;
                    }
                    break;

                }
            }
        }
    }
}
