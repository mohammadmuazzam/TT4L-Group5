using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{   
    public GameObject trapObject;
    void OnTriggerStay2D (Collider2D player)
    {
        //Debug.Log($"compare tag player? {player.gameObject.CompareTag("Player")}, is player on ground? {Player.isOnGround}");
        if (player.gameObject.CompareTag("Player") && !Player.isOnGround)
        {
            Debug.Log(trapObject.name);
            //Trap.shouldMoveTrap = true;
            //Debug.Log("Move trap");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
