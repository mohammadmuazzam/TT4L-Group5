using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] Boss boss;
    Rigidbody2D playerBody;
    [SerializeField] AudioClip[] damageSound;
    [Range(0,1)] [SerializeField] float volume;
    public static bool playerJumpOnBoss;
    
    private const string PLAYER_NAME = "Player";

    void Awake()
    {
        playerBody = GameObject.FindGameObjectWithTag(PLAYER_NAME).GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name == PLAYER_NAME)
            {
                playerBody = GameObject.FindGameObjectWithTag(PLAYER_NAME).GetComponent<Rigidbody2D>();
                if (playerBody.velocity.y < 0)
                {
                    playerJumpOnBoss = true;
                    gameObject.transform.parent.tag = "Untagged";
                    //print("playerJumpOnBoss: " + playerJumpOnBoss);
                }
                else
                {
                    print("player's velocity >= 0. not counting as jump on boss");
                }
                    
            }      
        }
        catch (System.Exception)
        {
            print("an exception occurred while trying to detect players collision with death box");
            return;
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
            BossParent.bossHealth -= 1;
            SoundFxManager.Instance.PlayRandomSoundFxClip(damageSound, transform, volume);
            ApplyJumpForce();
        }
    }

    private void ApplyJumpForce()
    {
        if (playerBody != null)
        {
            // Reset the player's vertical velocity to zero
            playerBody.velocity = new Vector2(Player.playerBody.velocity.x, 0);

            // Apply a consistent force
            playerBody.AddForce(new Vector2(Player.lastXMovement*4, 30), ForceMode2D.Impulse);
            //Debug.Log("PUSHING PLAYER UP, lastX: " + Player.lastXMovement);
        }
    }
}
