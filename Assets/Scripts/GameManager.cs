using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
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
    TimeSpan startTime;
    TimeSpan endTime;

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
    }

    void LateUpdate()
    {
        // if player dies
        if (!Player.isPlayerAlive)
        {
            SceneManager.LoadScene(currentLevelName);
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
            attempts += 1   ;
        }
        Debug.Log($"current: {currentLevelName}, scene: {scene.name}, attempt: {attempts}");
    }
}
