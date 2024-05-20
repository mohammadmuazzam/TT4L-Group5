using System.Collections;
using System.Collections.Generic;
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
            trapActivated = true;
            yield return new WaitForSeconds(4f);
            trapActivated = false;
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
            if (Player.shouldJump && triggerGameObject.name == "Trap Trigger")
            {
                // Start the coroutine to move the trap temporarily
                StartCoroutine(MoveTrapNonStop(trapScripts[0], 2));
            }
        }
    }
}
}
