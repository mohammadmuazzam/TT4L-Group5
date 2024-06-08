using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFxSource;
    public static SoundFxManager Instance;

    private AudioSource audioSource;

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

    public void PlaySoundFxClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        // instantiate audio source prefab
        audioSource = Instantiate(soundFxSource, spawnTransform.position, Quaternion.identity);

        // assign audio clip and volume
        audioSource.clip = clip;
        audioSource.volume = volume;

        // play audio
        audioSource.Play();

        // get audio clip length and destroy audio source object after sound has played
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public bool IsSoundFxPlaying()
    {
        if (audioSource != null)
        {
            return audioSource.isPlaying;
        }      
        else
        {
            return false;
        }
            
    }
}
