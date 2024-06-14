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
    
    private static GameManager instance;

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

    public void LevelEnd()
    {
        // move to EndPage and calculate elapsed time
        endTime = DateTime.Now.TimeOfDay;
        
        // this check is so that we don't double check
        if (elapsedTime.ToString() == "00:00:00")
            elapsedTime = endTime - startTime;
        
        SceneManager.LoadScene("EndPage");
    }

    //* wait for soundfx to finish playing before restarting level
    public async void RestartLevel()
    {
        isRestarting = true;
        while (SoundFxManager.Instance.IsSoundFxPlaying())
        {
            Time.timeScale = 0f;
            await Task.Yield();
        }
        Time.timeScale = 1f;

        coinCount = 0;
        Debug.Log ("Coun coin reset to 0");
        SceneManager.LoadScene(currentLevelName);
        isRestarting = false;

    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        // get name of current level, not current scene name
        if (scene.name.Substring(0,5).ToLower() == "level")
        {
            // reset attempt if not in the same level
            if (currentLevelName != scene.name)
            {
                attempts = 0;
            }
            currentLevelName = scene.name;
            
            // get current time
            startTime = DateTime.Now.TimeOfDay;
        }

        // calculate attempts
        if (scene.name == currentLevelName)
        {
            attempts += 1;
        }
    }

    public static void coinCounter()
    {
        coinCount += 1;
        Debug.Log ("Coins collected " + coinCount);
    }
}