using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

//? keep track of level data for next level
//?  restart game if player touches trap
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public static string currentLevelName;

    public static TimeSpan elapsedTime;
    public static int attempts;
    public static int coinCount;
    TimeSpan startTime;
    TimeSpan endTime;

    private float deltaTime = 0.0f;
    private bool isRestarting;

    void Awake()
    {
        // create only one instance of gamemanager
        if (instance == null)
        {
            instance = this;
            attempts = 1;
            attempts = 0;
            coinCount = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isRestarting = false;
    }

    void Update()
    {
        // Calculate deltaTime
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate FPS
        float fps = 1.0f / deltaTime;

        // Print FPS
        //Debug.Log("FPS: " + Mathf.Round(fps));
    }

    void LateUpdate()
    {
        // if player dies
        if (!Player.isPlayerAlive && !isRestarting)
        {
            RestartLevel();
        }
    }

    void OnEnable()
    {
        GateDoor.playerWins += LevelEnd;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        GateDoor.playerWins -= LevelEnd;
    }

    public async void LevelEnd()
    {
        // move to EndPage and calculate elapsed time
        endTime = DateTime.Now.TimeOfDay;
        
        // this check is so that we don't double check
        if (elapsedTime.ToString() == "00:00:00")
            elapsedTime = endTime - startTime;

        while (SoundFxManager.Instance.IsSoundFxPlaying())
        {
            Time.timeScale = 0f;
            await Task.Yield();
        }
        await Task.Delay(100);
        
        SceneManager.LoadScene("EndPage");
    }

    //* wait for soundfx to finish playing before restarting level
    public async void RestartLevel()
    {
        isRestarting = true;
        attempts = attempts + 1;
        print("restarting level, attempt: " + attempts);
        while (SoundFxManager.Instance.IsSoundFxPlaying())
        {
            Time.timeScale = 0f;
            await Task.Yield();
        }
        await Task.Delay(500);
        Time.timeScale = 1f;

        coinCount = 0;
        Debug.Log ("Coun coin reset to 0");
        SceneManager.LoadScene(currentLevelName);
        Player.isPlayerAlive = true;
        isRestarting = false;

    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        try
        {
            // get name of current level, not current scene name
            if (scene.name.Substring(0,5).ToLower() == "level")
            {
                // reset attempt if not in the same level
                if (currentLevelName != scene.name)
                {
                    print("resetting attempt");
                    attempts = 1;
                }
                currentLevelName = scene.name;
                
                // get current time
                startTime = DateTime.Now.TimeOfDay;
            }  
        }
        catch (Exception)
        {
            return;
        }
        
    }

    public static void coinCounter()
    {
        coinCount += 1;
        Debug.Log ("Coins collected " + coinCount);
    }
}