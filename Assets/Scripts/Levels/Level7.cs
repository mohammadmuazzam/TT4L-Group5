using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField] private PolygonCollider2D trapCollider;
    [SerializeField] private ParticleSystem explosionParticles1;
    [SerializeField] private ParticleSystem explosionParticles2;
    [SerializeField] private AudioClip slowBeep, fastBeep, explosionClip;
    [Range(0, 1)] [SerializeField] private float slowBeepVolume, fastBeepVolume, explosionVolume;
    [Range (100,1500)][SerializeField] int bombTimer;
    
    private bool closeTrapTriggered1, platform1Moved, notExploded1, platform2Moved, gateMoved, spikeActivated, notExploded2;
    private CancellationTokenSource cancellationTokenSource;
    [SerializeField] private GameObject explodingPlatform1, explodingPlatform2, platformGate;

    void Awake()
    {
        closeTrapTriggered1 = false;
        platform1Moved = false;
        notExploded1 = false;
        platform2Moved = false;
        gateMoved = false;
        spikeActivated = false;
        notExploded2 = false;
        
        platformGate.SetActive(false);
        trapCollider.enabled = false;

        cancellationTokenSource = new CancellationTokenSource();
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
                        trapCollider.enabled = true;
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

                    case "Explosion Trigger 1": //* bomb
                    if (!notExploded1 && trapTriggerScript.movingPlatformIsInTrigger) //* explosion 
                    {
                        try
                        {
                            notExploded1 = true;
                            closeTrapTriggered1 = true;

                            //* beeps
                            await SoundFxManager.Instance.PlaySoundFxClipAsync(slowBeep, trapScripts[0].gameObject.transform, slowBeepVolume, cancellationTokenSource);
                            await SoundFxManager.Instance.PlaySoundFxClipAsync(slowBeep, trapScripts[0].gameObject.transform, slowBeepVolume, cancellationTokenSource);
                            await SoundFxManager.Instance.PlaySoundFxClipAsync(fastBeep, trapScripts[0].gameObject.transform, fastBeepVolume, cancellationTokenSource);

                            SoundFxManager.Instance.PlaySoundFxClip(explosionClip, trapScripts[0].gameObject.transform, explosionVolume);
                            explosionParticles1.Play();
                            explodingPlatform1.SetActive(false);
                            trapScripts[0].gameObject.SetActive(false);
                        }
                        catch (Exception)
                        {
                            return;
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
                        gateMoved = true;
                        await trapScripts[3].PermanentMoveTrap();
                        await Task.Delay(700);
                        platformGate.SetActive(true);

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
                        try
                        {
                            notExploded2 = true;
                            await SoundFxManager.Instance.PlaySoundFxClipAsync(fastBeep, trapScripts[0].gameObject.transform, fastBeepVolume, cancellationTokenSource);
                            SoundFxManager.Instance.PlaySoundFxClip(explosionClip, trapScripts[0].gameObject.transform, explosionVolume);
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

    void OnApplicationQuit()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }

}
