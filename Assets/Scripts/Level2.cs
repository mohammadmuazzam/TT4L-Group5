using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;
    
    [SerializeField]
    private GameObject[] trapTriggers;

    private bool trap1Activated,trap2Activated,trap3Activated;

    public float playerMinX, playerMaxX;


    // Start is called before the first frame update
    void Awake()
    {
        trap1Activated = false;
        trap2Activated = false;
        trap3Activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrapTrigger ();
    }

    IEnumerator MoveTrapNonStop (Trap trapScript, int mode)
    {
        if (mode == 1) // temp move
        yield return StartCoroutine(trapScript.TemporaryMoveTrap());
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
                    if (!trap1Activated)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[0], 1));
                        trap1Activated = true;
                    }
                    break;

                    case "Trap Trigger 2":
                    if(!trap2Activated && Player.shouldJump)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[1],1));
                        trap2Activated = true;
                    }
                    break;

                    case "Trap Trigger 3":
                    if (!trap3Activated)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[2],1));
                        trap3Activated = true;
                    }
                    break;

                }
            }
        }
    }
}

