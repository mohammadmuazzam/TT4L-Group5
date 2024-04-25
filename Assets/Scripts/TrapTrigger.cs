using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapTrigger : MonoBehaviour
{   
    public bool playerIsInTrigger = false;

    void Awake()
    {

    }
    
    // when player is in trap trigger
    void OnTriggerStay2D (Collider2D player)
    {
        //Debug.Log($"player is in {gameObject.name}");
        if (player.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = true;
            //print("traptrigger: player is in trigger");
        }
    }

    void OnTriggerExit2D (Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = false;
            //print("traptrigger: player is NOT in trigger");
        }
    }
}
