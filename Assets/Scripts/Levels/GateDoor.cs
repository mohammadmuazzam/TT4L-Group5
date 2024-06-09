using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GateDoor : MonoBehaviour
{
    public bool playerReachesGate;

    // delegate & events
    public delegate void PlayerWins();
    public static event PlayerWins playerWins;

    void Awake()
    {
        playerReachesGate = false;  
    }
    void OnTriggerEnter2D (Collider2D player)
    {
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
}
