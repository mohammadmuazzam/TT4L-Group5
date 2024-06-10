using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    private bool playerOnTrampoline;

    [SerializeField] Rigidbody2D player;
    [Range(30, 200)] [SerializeField] float jumpForce;

    private const string PLAYER_NAME = "Player";
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag (PLAYER_NAME))
        {
            playerOnTrampoline = true;
            print("Player on trampline");
        }
        
    }

    void FixedUpdate()
    {
        if (playerOnTrampoline)
        {
            playerOnTrampoline = false;
            ApplyTrampolineForce();
        }
    }

    private void ApplyTrampolineForce()
    {
        Player.playerBody.velocity = new UnityEngine.Vector2(Player.playerBody.velocity.x, 0);

            // Apply a consistent force
        Player.playerBody.AddForce(new UnityEngine.Vector2(Player.lastXMovement*4, jumpForce), ForceMode2D.Impulse);
    }
}
