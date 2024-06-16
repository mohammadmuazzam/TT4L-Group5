using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
public class SoundFxManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFxSource;
    public static SoundFxManager Instance;
    private CancellationTokenSource cancellationTokenSource;

    private AudioSource audioSource, audioSourceAsync;
    //private bool fadingOut;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            cancellationTokenSource = new CancellationTokenSource();
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
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
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

    public async Task PlayRandomSoundFxClipAsync(AudioClip[] clips, Transform spawnTransform, float volume, CancellationTokenSource cancellationTokenSourceOutSide)
    {
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        if (randomIndex == clips.Length)
            randomIndex = 0;
        //* instantiate audio source prefab
        audioSourceAsync = Instantiate(soundFxSource, spawnTransform.position, Quaternion.identity);

        //* assign audio clip and volume
        audioSourceAsync.clip = clips[randomIndex];
        audioSourceAsync.volume = volume;

        //* get audio clip length and destroy audio source object after sound has played
        float clipLength = audioSourceAsync.clip.length;
        float elapsedTime = 0;

        bool pausing = true;
        int beforeAttempt = GameManager.attempts;

        while (elapsedTime < clipLength)
        {
            //* return if player dies or restart or cancellation is requested
            if (beforeAttempt != GameManager.attempts || cancellationTokenSource.IsCancellationRequested || cancellationTokenSourceOutSide.IsCancellationRequested)
            {
                print("RETURN, OutSideToken: " + cancellationTokenSourceOutSide);
                Destroy(audioSourceAsync.gameObject);
                return;
            }

            //* pause audio
            if (PauseMenu.isPaused)
            {
                print("pausing audio");
                audioSource.Pause();
                pausing = true;
            }
            //* play audio
            else
            {
                elapsedTime += Time.deltaTime;
                
                if (pausing)
                {
                    print("playing audio");
                    audioSourceAsync.Play();
                    pausing = false;
                }
            }
            await Task.Yield();
        }
        print("DESTROYING AUDIOSOURCE OBJECT");
        Destroy(audioSourceAsync.gameObject);
    }

    public async Task PlaySoundFxClipAsync(AudioClip clips, Transform spawnTransform, float volume, CancellationTokenSource cancellationTokenSourceOutSide)
    {
        //* instantiate audio source prefab
        audioSourceAsync = Instantiate(soundFxSource, spawnTransform.position, Quaternion.identity);

        //* assign audio clip and volume
        audioSourceAsync.clip = clips;
        audioSourceAsync.volume = volume;

        //* get audio clip length and destroy audio source object after sound has played
        float clipLength = audioSourceAsync.clip.length;
        float elapsedTime = 0;

        bool pausing = true;
        int beforeAttempt = GameManager.attempts;

        while (elapsedTime < clipLength)
        {
            //* return if player dies or restart or cancellation is requested
            if (beforeAttempt != GameManager.attempts || cancellationTokenSource.IsCancellationRequested || cancellationTokenSourceOutSide.IsCancellationRequested)
            {
                print("RETURN, OutSideToken: " + cancellationTokenSourceOutSide);
                Destroy(audioSourceAsync.gameObject);
                return;
            }

            //* pause audio
            if (PauseMenu.isPaused)
            {
                print("pausing audio");
                audioSource.Pause();
                pausing = true;
            }
            //* play audio
            else
            {
                elapsedTime += Time.deltaTime;
                
                if (pausing)
                {
                    print("playing audio");
                    audioSourceAsync.Play();
                    pausing = false;
                }
            }
            await Task.Yield();
        }
        print("DESTROYING AUDIOSOURCE OBJECT");
        Destroy(audioSourceAsync.gameObject);
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

        void OnApplicationQuit()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
