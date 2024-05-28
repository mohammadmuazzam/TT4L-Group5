using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level6 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;

    [SerializeField]
    private GameObject[] trapTriggers;

    private bool trap1Activated, trap2Activated;

    public float playerMinX, playerMaxX;


    // Start is called before the first frame update
    void Awake()
    {
        trap1Activated = false;
        trap2Activated = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckTrapTrigger ();
    }

    void CheckTrapTrigger()
    {
        foreach (GameObject triggerGameObject in trapTriggers)
        {
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();
            
            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // trigger traps according to trapTrigger
                switch (trapTriggerScript.name)
                {
                    case "Trap Trigger 1":
                    if(!trap1Activated && Player.shouldJump)
                    {
                        _ = trapScripts[0].PermanentMoveTrap();
                        trap1Activated = true;
                    }
                    break;

                    case "Trap Trigger 2":
                    if(!trap2Activated)
                    {
                        _= trapScripts[1].PermanentMoveTrap();
                        trap2Activated = true;
                    }
                    break;
                }
            }
        }
    }
}