using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TODO: keep track of player tries
    // TODO: restart game if player touches trap
    private string currentSceneName;
    private GameObject gate;
    private GateDoor gateScript;
    private static GameManager instance;

    public TimeSpan elapsedTime;
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
        

        //get gate script
        gate = GameObject.FindWithTag("Gate");
        gateScript = gate.GetComponent<GateDoor>();

        // get current time
        startTime = DateTime.Now.TimeOfDay;
        Debug.Log("Current Time: " + startTime.ToString());

        // get current scene name
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!Player.isPlayerAlive)
        {
            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void SceneChange()
    {
        // move to EndPage and calculate elapsed time
        if (gateScript != null)
        {
            endTime = DateTime.Now.TimeOfDay;
            SceneManager.LoadScene("EndPage");
            if (elapsedTime.ToString() == "00:00:00")
                elapsedTime = endTime - startTime;
            Debug.Log($"elapsed time: {elapsedTime.TotalSeconds}");
        }
        else
            print("game script is null");
        
    }
}
