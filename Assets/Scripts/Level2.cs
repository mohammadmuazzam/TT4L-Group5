using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;
    
    [SerializeField]
    private GameObject[] trapTriggers;

    private bool trapActivated;

    public float playerMinX, playerMaxX;


    // Start is called before the first frame update
    void Awake()
    {
        trapActivated = false;
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
                    case "Trap Trigger":
                    if (!trapActivated)
                    {
                        StartCoroutine(MoveTrapNonStop(trapScripts[0], 1));
                        trapActivated = true;
                    }
                    break;
                }
            }
        }
    }
}

