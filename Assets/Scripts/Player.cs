using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 15f;
    private float jumpForce = 25f;

    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private SpriteRenderer sr;

    private float movementX;
    private bool isOnGround = false;
    public static bool isPlayerAlive;
    public static bool shouldJump = false;
    private bool isCrouch = false;

    private const string CROUCH_ANIMATION_CONDITION = "Crouch";
    private const string IDLE_ANIMATION_CONDITION = "IdleStand";

    //! get minX, maxX from level manager
    [SerializeField]
    private float minX, maxX;
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();   
        isPlayerAlive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        if (shouldJump)
        {
            shouldJump = false;
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void PlayerMovement()
    {
        // horizontal movement
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f) * speed * Time.deltaTime;

        // crouch
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            isCrouch = true;
        }
        else isCrouch = false;

        //left
        if (transform.position.x <= minX)
        {
            Vector2 tempPos = transform.position;
            tempPos.x = minX;
            transform.position = tempPos;
        }
        //right
        else if (transform.position.x >= maxX)
        {
            Vector2 tempPos = transform.position;
            tempPos.x = maxX;
            transform.position = tempPos;
        }

        // jump
        if (isOnGround && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            isOnGround = false;
            shouldJump = true;
        }
    }

    //check for collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with ground or platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }

        // collision with trap
        if (collision.gameObject.CompareTag("Trap"))
        {
            isPlayerAlive = false;
        }
    }

    void AnimatePlayer()
    {
        // if crouch, then change to crouch animation
        if (isCrouch) 
        {
            playerAnimator.SetBool(CROUCH_ANIMATION_CONDITION, true);
            playerAnimator.SetBool(IDLE_ANIMATION_CONDITION, false);
        }
        else 
        {
            playerAnimator.SetBool(CROUCH_ANIMATION_CONDITION, false);
            playerAnimator.SetBool(IDLE_ANIMATION_CONDITION, true);
        }
        
        //TODO: if crouch, then don't go to idle animation
        if (movementX != 0)
        {
            playerAnimator.SetBool(IDLE_ANIMATION_CONDITION, false);
            if (movementX > 0)
            {
                sr.flipX = false;
            }
            else if (movementX < 0)
            {
                sr.flipX = true;
            }
        }
        
    }
}
