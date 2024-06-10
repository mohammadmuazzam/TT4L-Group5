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
    [SerializeField] private ParticleSystem explosionParticles1;
    [SerializeField] private ParticleSystem explosionParticles2;
    [Range (100,1500)][SerializeField] int bombTimer;
    
    private bool closeTrapTriggered1, platform1Moved, notExploded1, platform2Moved, gateMoved, spikeActivated, notExploded2;
    [SerializeField] private GameObject explodingPlatform1;
    [SerializeField] private GameObject explodingPlatform2;

    void Awake()
    {
        closeTrapTriggered1 = false;
        platform1Moved = false;
        notExploded1 = false;
        platform2Moved = false;
        gateMoved = false;
        spikeActivated = false;
        notExploded2 = false;
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

                    case "Explosion Trigger 1":
                    if (!notExploded1 && trapTriggerScript.movingPlatformIsInTrigger)
                    {
                        await Task.Delay(bombTimer);
                        try
                        {
                            explosionParticles1.Play();
                            explodingPlatform1.SetActive(false);
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

                    case "Spike Trigger":
                    if (!spikeActivated)
                    {
                        _= trapScripts[4].PermanentMoveTrap();
                        spikeActivated = true;
                    }
                    break;

                    case "Explosion Trigger 2":
                    if (!notExploded2 && Player.shouldJump)
                    {
                        await Task.Delay (bombTimer);
                        try
                        {
                            explosionParticles2.Play ();
                            explodingPlatform2.SetActive(false);
                        }
                        catch(Exception)
                        {}
                    }
                    break;
                }
            }
        }
    }

}