using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] Boss boss;
    public static bool playerJumpOnBoss;
    
    private const string PLAYER_NAME = "Player";

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == PLAYER_NAME)
        {
            playerJumpOnBoss = true;
            gameObject.transform.parent.tag = "Untagged";
            //print("player jump on boss");
        }      
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.parent.tag = "Trap";
    }

    void FixedUpdate()
    {
        if (playerJumpOnBoss)
        {
            playerJumpOnBoss = false;
            boss.bossHealth -=1;
            ApplyJumpForce();
        }
    }

        private void ApplyJumpForce()
    {
        if (Player.playerBody != null)
        {
            // Reset the player's vertical velocity to zero
            Player.playerBody.velocity = new Vector2(Player.playerBody.velocity.x, 0);

            // Apply a consistent force
            Player.playerBody.AddForce(new Vector2(Player.lastXMovement*4, 30), ForceMode2D.Impulse);
            //Debug.Log("PUSHING PLAYER UP, lastX: " + Player.lastXMovement);
        }
    }
}
