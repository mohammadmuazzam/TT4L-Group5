using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapTrigger : MonoBehaviour
{   
    public bool playerIsInTrigger = false;
    
    // when player is in trap trigger
    void OnTriggerStay2D (Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = true;
        }
    }

    void OnTriggerExit2D (Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = false;
        }
    }
}
