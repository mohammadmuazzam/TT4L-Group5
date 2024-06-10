using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] Rigidbody2D playerBody;
    [SerializeField] AudioClip[] damageSound;
    [Range(0,1)] [SerializeField] float volume;
    public static bool playerJumpOnBoss;
    
    private const string PLAYER_NAME = "Player";

    void OnTriggerEnter2D (Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name == PLAYER_NAME && playerBody.velocity.y < 0)
            {
                playerJumpOnBoss = true;
                gameObject.transform.parent.tag = "Untagged";
            }      
        }
        catch (System.Exception)
        {
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
