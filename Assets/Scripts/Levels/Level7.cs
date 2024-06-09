using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    [SerializeField] private Trap[] trapScripts;
    [SerializeField] private GameObject[] trapTriggers;
    [SerializeField] private ParticleSystem explosionParticles;
    [Range (100,1500)][SerializeField] int bombTimer;
    
    private bool closeTrapTriggered1, platform1Moved, notExploded, platform2Moved, gateMoved;
    [SerializeField] private GameObject explodingPlatform;

    void Awake()
    {
        closeTrapTriggered1 = false;
        platform1Moved = false;
        notExploded = false;
        platform2Moved = false;
        gateMoved = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckForTrapTrigger();
    }

    async void CheckForTrapTrigger()
    {
        // check if any trap trigger has been triggered
        foreach (GameObject triggerGameObject in trapTriggers)
        {
            if (triggerGameObject == null)
                return;
            TrapTrigger trapTriggerScript = triggerGameObject.GetComponent<TrapTrigger>();
            
            if (trapTriggerScript != null && trapTriggerScript.playerIsInTrigger)
            {   
                // trigger traps according to trapTrigger
                switch (trapTriggerScript.name)
                {
                    case "Close Trap Trigger 1":
                    if (trapTriggerScript.movingPlatformIsInTrigger && !closeTrapTriggered1)
                    {
                        _ = trapScripts[0].TemporaryMoveTrap();
                        closeTrapTriggered1 = true;
                    }
                    break;

                    case "Platform Trigger 1":
                    if (!platform1Moved)
                    {
                        _= trapScripts[1].PermanentMoveTrap();
                        platform1Moved = true;
                    }
                    break;

                    case "Explosion Trigger":
                    if (!notExploded && trapTriggerScript.movingPlatformIsInTrigger)
                    {
                        await Task.Delay(bombTimer);
                        try
                        {
                            explosionParticles.Play();
                            explodingPlatform.SetActive(false);
                            trapScripts[0].gameObject.SetActive(false);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;

                    case "Platform Trigger 2":
                    if (!platform2Moved)
                    {
                        _= trapScripts[2].PermanentMoveTrap();
                        platform2Moved = true;
                    }
                    break;

                    case "Gate Trigger":
                    if (!gateMoved && Player.shouldJump)
                    {
                        _= trapScripts[3].PermanentMoveTrap();
                        gateMoved = true;
                    }
                    break;
                }
            }
        }
    }

    private async void TimeToExplode()
    {
        Debug.Log ("Timehas started");
        await Task.Delay(bombTimer);
        explodingPlatform.SetActive(false);
    }

}
