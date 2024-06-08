using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    [SerializeField] private GameObject[] trapTriggers;
    [SerializeField] private Boss catBoss;
    
    [SerializeField] private bool[] hasTriggered;

    void Awake()
    {
        for (int i = 0; i < hasTriggered.Length; i++)
        {
            hasTriggered[i] = false;
            print("i: " + i);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckForTrapTrigger();

        CheckAttemptAndBossHealth();
    }

    void CheckForTrapTrigger()
    {
        // check if any trap trigger has been triggered
        foreach (GameObject triggerGameObject in trapTriggers)
        {
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();

            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // trigger traps according to trapTrigger
                switch (trapTriggerScript.name)
                {
                    case "Cat Trigger 1":
                    if (!hasTriggered[0])
                    {
                        catBoss.ShootNormalBullets();
                        hasTriggered[0] = true;
                    }
                    break;
                }
            }
        }
    }

    void CheckAttemptAndBossHealth()
    {
        // traps leading to first kill
        if (GameManager.attempts == 1 || catBoss.bossHealth == 3)
        {
            catBoss.bossHealth = 3;

            
        }
    }
}
