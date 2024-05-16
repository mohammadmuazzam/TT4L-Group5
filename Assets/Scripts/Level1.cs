using System;
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

    private bool trap1Activated, trap2Activated, trap3Activated;
    private bool moveTrap1, moveTrap2, moveTrap3 = false;
    public float playerMinX, playerMaxX;

    void Awake()
    {
        trap1Activated = false;
        trap2Activated = false;
        trap3Activated = false;
    }

    void Start()
    {
        //StartCoroutine(CoroutineTimeCheck(trapScripts[0].PermanentMoveTrap()));
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTrapTrigger();
    }

    //coroutine to move trap

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
                    if (Player.shouldJump && !trap1Activated)
                    {
                        trapScripts[0].PermanentMoveTrap(); 
                        trap1Activated = true;
                    }
                    break;

                    case "Trap Trigger 2":
                    if (!trap2Activated)
                    {
                        moveTrap2 = true;
                        trap2Activated = true;
                    }         
                    break;

                    case "Trap Trigger 3":
                    if (!trap3Activated)
                    {
                        moveTrap3 = true;
                        trap3Activated = true;
                    }
                    break;

                }
            }
        }
    }
}

