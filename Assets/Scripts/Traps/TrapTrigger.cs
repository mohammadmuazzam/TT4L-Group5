using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapTrigger : MonoBehaviour
{   
    public bool playerIsInTrigger = false;
    public bool movingPlatformIsInTrigger;

    private static string PLAYER_TAG = "Player";
    private static string MOVING_PLATFORM_TAG = "Moving Platform";
    
    // when player is in trap trigger
    void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            playerIsInTrigger = true;
        }

        if (collision.gameObject.CompareTag(MOVING_PLATFORM_TAG))
        {
            movingPlatformIsInTrigger = true;
        }
    }

    void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            playerIsInTrigger = false;
        }

        if (collision.gameObject.CompareTag(MOVING_PLATFORM_TAG))
        {
            movingPlatformIsInTrigger = false;
        }
    }
}
