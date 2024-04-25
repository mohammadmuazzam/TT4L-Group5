using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public bool playerReachesGate;

    void Awake()
    {
        playerReachesGate = false;
    }
    void OnTriggerEnter2D (Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerReachesGate = true;
        }
    }
}
