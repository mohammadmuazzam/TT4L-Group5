using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFxSource;
    public static SoundFxManager Instance;

    private AudioSource audioSource;
    //private bool fadingOut;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        //fadingOut = false;
    }

    void Update()
    {
        
    }

    public void PlaySoundFxClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        // instantiate audio source prefab
        audioSource = Instantiate(soundFxSource, spawnTransform.position, Quaternion.identity);

        // assign audio clip and volume
        audioSource.clip = clip;
        audioSource.volume = volume;

        // play audio if player is still alive
        audioSource.Play();

        // get audio clip length and destroy audio source object after sound has played
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        
    }

    public void PlayRandomSoundFxClip(AudioClip[] clips, Transform spawnTransform, float volume)
    {
        int randomIndex = Random.Range(0, clips.Length);
        // instantiate audio source prefab
        audioSource = Instantiate(soundFxSource, spawnTransform.position, Quaternion.identity);

        // assign audio clip and volume
        audioSource.clip = clips[randomIndex];
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

    async void FadeOutSoundFx()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            await Task.Yield();
        }
    }
}
