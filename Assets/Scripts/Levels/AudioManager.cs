using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] soundFx;
    public AudioSource soundFxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlaySound(string soundName)
    {
        //* get sound if it exists in array
        Sound s = Array.Find(soundFx, x => x.clipName == soundName);

        if (s == null)
        {
            print("No sound found");
        }
        else
        {
            soundFxSource.PlayOneShot(s.clip);
        }

    }
}
