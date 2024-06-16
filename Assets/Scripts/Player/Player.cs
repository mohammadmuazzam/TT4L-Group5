using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float speed = 15f;
    private const float jumpForce = 25f;

    public static Rigidbody2D playerBody;
    public static float lastXMovement;

    private Animator playerAnimator;
    private SpriteRenderer sr;
    private GameObject movingPlatformObject;

    //* movement 
    private float movementX;
    private bool isOnGround, isCrouch, isWalking, isOnMovingPlatform, jumpAgain = false;
    public static bool isPlayerAlive;
    public static bool shouldJump;
    public static bool canPlayerMove;


    //* CONSTANTS
    private const string CROUCH_ANIMATION_CONDITION = "Crouch";
    private const string IDLE_ANIMATION_CONDITION = "IdleStand";
    private const string WALK_ANIMATION_CONDITION = "Walk";

    PlatformMovement movingPlatformScript;
    Vector3 tempPos;

    //* sound
    [SerializeField] AudioClip[] deathSoundFx;
    
    [Range(0, 1)][SerializeField] float volume;

    [SerializeField] public float minX, maxX;
    [SerializeField] bool canPlayerDie;
    void Awake()
    {
        //* get component
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        //* default value
        isPlayerAlive = true;
        canPlayerMove = true;
        shouldJump = false;
        jumpAgain = true;
        Time.timeScale = 1f;
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimatePlayer();

        if (isOnMovingPlatform)
            MoveXPlayerWithPlatform(movingPlatformObject);
    }

    void FixedUpdate()
    {
        if (shouldJump && isOnGround)
        {
            //print("JUMP FORCE");
            isOnGround = false;
            shouldJump = false;
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    async void PlayerMovement()
    {
        // horizontal movement
        if (canPlayerMove)
        {
            movementX = Input.GetAxisRaw("Horizontal");
            if (movementX != 0f)
            {
                lastXMovement = movementX;
                isWalking = true;
            }
            else 
            {
                isWalking = false;
            }
                

            transform.position += new Vector3(movementX, 0f) * speed * Time.deltaTime;

            // crouch
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl))
            {
                isCrouch = true;
            }
            else isCrouch = false;

            //left limit
            if (transform.position.x <= minX)
            {
                Vector2 tempPos = transform.position;
                tempPos.x = minX;
                transform.position = tempPos;
            }
            //right limit
            else if (transform.position.x >= maxX)
            {
                Vector2 tempPos = transform.position;
                tempPos.x = maxX;
                transform.position = tempPos;
            }

            // jump
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                if (isOnGround && jumpAgain)
                {
                    //print("JUMP PRESSED: " + DateTime.Now.TimeOfDay);
                    shouldJump = true;
                    jumpAgain = false;
                    await Task.Delay(100);
                    jumpAgain = true;
                } 
            }
        }
        
    }

    //check for collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with trap
        if (collision.gameObject.CompareTag("Trap") && canPlayerDie)
        {
            SoundFxManager.Instance.PlayRandomSoundFxClip(deathSoundFx, transform, volume);
            print("PLAYER KILLED");
            isPlayerAlive = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // collision with ground or platform
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Moving Platform"))
        {
            if (!shouldJump)
                isOnGround = true;
            if (collision.gameObject.CompareTag("Moving Platform"))
            {
                isOnMovingPlatform = true;
                //* get parents gameObject
                movingPlatformObject = collision.gameObject.transform.parent.gameObject;
            }
        }
    }

    // checks when 2 object stops colliding
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Moving Platform"))
        {   
            isOnGround = false;
            if (collision.gameObject.CompareTag("Moving Platform"))
            {
                isOnMovingPlatform = false;
                movingPlatformObject = null;
            }
        }
    }

    //? move player with platform's parent in X direction
    void MoveXPlayerWithPlatform(GameObject platformObject)
    {
        movingPlatformScript = platformObject.GetComponent<PlatformMovement>();
        tempPos = transform.position;
        tempPos.x += movingPlatformScript.moveAmountPos.x;
        transform.position = tempPos;   
    }

    void AnimatePlayer()
    {
        //* if crouch
        if (isCrouch) 
        {
            //* crouch ONLY
            playerAnimator.SetBool(CROUCH_ANIMATION_CONDITION, true);
            playerAnimator.SetBool(IDLE_ANIMATION_CONDITION, false);

            //* if crouch and walking
            if (isWalking)
            {
                playerAnimator.SetBool(WALK_ANIMATION_CONDITION, true);
            }
            else
            {
                playerAnimator.SetBool(WALK_ANIMATION_CONDITION, false);
            }
        }
        else if (isWalking) //* if walking ONLY
        {
            playerAnimator.SetBool(WALK_ANIMATION_CONDITION, true);
            playerAnimator.SetBool(CROUCH_ANIMATION_CONDITION, false);
            playerAnimator.SetBool(IDLE_ANIMATION_CONDITION, false);
        }
        else //* idle stance
        {
            playerAnimator.SetBool(WALK_ANIMATION_CONDITION, false);
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

    public void ForceKillPlayer()
    {
        SoundFxManager.Instance.PlayRandomSoundFxClip(deathSoundFx, transform, volume);
        print("PLAYER KILLED");
        isPlayerAlive = false;    
    }
}   