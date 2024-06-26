using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    [SerializeField]
    private Trap[] trapScripts;
    
    [SerializeField]
    private GameObject[] trapTriggers;
    [SerializeField] private GameObject spikeDisable;

    [SerializeField] private Lightning lightningScripts;

    private bool trap1Activated,trap2Activated,trap3Activated,trap4Activated,trap5Activated;
    private CancellationTokenSource cancellationTokenLightning;

    public float playerMinX, playerMaxX;


    // Start is called before the first frame update
    void Awake()
    {
        trap1Activated = false;
        trap2Activated = false;
        trap3Activated = false;
        trap4Activated = false;
        trap5Activated = false;

        cancellationTokenLightning = new CancellationTokenSource();
        spikeDisable.SetActive(false);
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
                    if (!trap1Activated)
                    {
                        _ = trapScripts[0].TemporaryMoveTrap();
                        trap1Activated = true;
                    }
                    break;

                    case "Trap Trigger 2":
                    if(!trap2Activated)
                    {
                        _ = trapScripts[1].PermanentMoveTrap();
                        trap2Activated = true;
                    }
                    break;

                    case "Trap Trigger 3":
                    if(!trap3Activated && trap2Activated)
                    {
                        _ = lightningScripts.SpawnLightning(cancellationTokenLightning);
                        trap3Activated = true;
                    }
                    break;

                    case "Trap Trigger 4":
                    if(!trap4Activated )
                    {
                        _ = trapScripts[2].PermanentMoveTrap();
                        trap4Activated = true;
                    }
                    break;

                    case "Trap Trigger 5":
                    if(!trap5Activated && trap4Activated )
                    {
                        spikeDisable.SetActive(true);
                        _ = trapScripts[3].PermanentMoveTrap();
                        trap5Activated = true;
                    }
                    break;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        if (cancellationTokenLightning != null)
        {
            cancellationTokenLightning.Cancel();
            cancellationTokenLightning.Dispose();
        }
    }
}
