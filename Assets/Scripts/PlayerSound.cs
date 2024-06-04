using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip[] footsteps;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void PlayerFootsteps()
    {
        AudioClip clip = footsteps[Random.Range(0, footsteps.Length)];
        source.clip = clip;
        source.Play();
    }
}
