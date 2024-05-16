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
    [SerializeField] Animator transitionAnim;
    public static string currentLevelName;

    public static TimeSpan elapsedTime;
    public static int attempts;
    TimeSpan startTime;
    TimeSpan endTime;

    private float deltaTime = 0.0f;

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

        Application.targetFrameRate = 60;
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
        if (!Player.isPlayerAlive)
        {
            Debug.Log("restarting level");
            //! COROUTINE CALLED LIKE 10 TIMES
            LoadLevel();
        }
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
        transitionAnim.SetTrigger("Start");
        if (scene.name.Substring(0,5).ToLower() == "level")
        {
            // reset attempt 
            if (currentLevelName != scene.name)
            {
                attempts = 0;
            }
         
            currentLevelName = scene.name;
            startTime = DateTime.Now.TimeOfDay;
        }

        // calculate attempts
        if (scene.name == currentLevelName)
        {
            attempts += 1   ;
        }
    }

    private async void LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        await Task.Delay(500);
        SceneManager.LoadScene(currentLevelName);
        await Task.Yield();
    }

    void OnEnable()
    {
        GateDoor.playerWins += LevelEnd;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        GateDoor.playerWins -= LevelEnd;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
