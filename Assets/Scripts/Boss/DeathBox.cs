using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool playerJumpOnBoss;
    [SerializeField] GameObject player;
    private const string PLAYER_NAME = "Player";

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == PLAYER_NAME)
        {
            playerJumpOnBoss = true;
            //print("player jump on boss");
        }      
    }

    void FixedUpdate()
    {
        if (playerJumpOnBoss)
        {
            print("PUSHING PLAYER UP");
            playerJumpOnBoss = false;
            Player.playerBody.AddForce(new Vector2(Player.lastXMovement, 35), ForceMode2D.Impulse);
        }
    }
}
