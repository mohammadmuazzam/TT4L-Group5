using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    private bool hasStartedMusic;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            hasStartedMusic = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            audioSource.Pause();
            hasStartedMusic = false;
        }
        else
        {
            if (!hasStartedMusic)
            {
                
            }
        }
    }
}
