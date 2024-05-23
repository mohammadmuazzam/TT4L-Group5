using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class Level5 : MonoBehaviour
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
     void LateUpdate()
    {
        CheckForTrapTrigger();
    }
    void CheckForTrapTrigger()
{
    // Check if any trap trigger has been triggered
    foreach (GameObject triggerGameObject in trapTriggers)
    {
        TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();

        if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger && !trapActivated)
        {
            // Trigger the trap if the player should jump and trap is not currently activated
            if (Player.shouldJump && triggerGameObject.name == "Trap Trigger" && !trapActivated)
            {
                trapActivated = true;
                _ = trapScripts[0].TemporaryMoveTrap();
            }
        }
    }
}
}
