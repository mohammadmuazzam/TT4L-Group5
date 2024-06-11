using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GateDoor : MonoBehaviour
{
    public bool playerReachesGate;

    public LevelMenuController[] levelMenuControllers;

    // delegate & events
    public delegate void PlayerWins();
    public static event PlayerWins playerWins;

    void Awake()
    {
        playerReachesGate = false;  
    }
    void OnTriggerEnter2D (Collider2D player)
    {
        UnlockNewLevel();
        if (player.gameObject.CompareTag("Player"))
        {
            playerReachesGate = true;
            Debug.Log("player reach gate");
            if (playerWins != null)
            {
                playerWins();
            }
        }
    }

    public void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("EndPoint"))
        {
            PlayerPrefs.SetInt("EndPoint", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("LockLevel",PlayerPrefs.GetInt("LockLevel",1) + 1);
            PlayerPrefs.Save();
        }
    }
}
