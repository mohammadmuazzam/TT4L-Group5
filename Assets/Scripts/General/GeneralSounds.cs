using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GeneralSounds : MonoBehaviour
{
    [HideInInspector] public AudioSource clickSound;

    public int clickSoundInMs;
    
    void Awake()
    {
        clickSoundInMs = (int) Math.Ceiling(clickSound.clip.length * 1000);
    }
    public void ClickSound()
    {
        clickSound.Play();
    }
}
