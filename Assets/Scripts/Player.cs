using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 15f;
    private float jumpForce = 25f;
    private Rigidbody2D playerBody;

    private float movementX;
    private bool shouldJump = false;
    private bool isOnGround = false;

    [SerializeField]
    private float minX, maxX;
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void FixedUpdate()
    {
        if (shouldJump)
        {
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            shouldJump = false;
        }
    }

    void PlayerMovement()
    {
        // horizontal movement
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f) * speed * Time.deltaTime;
        if (transform.position.x <= minX)
        {
            Vector2 tempPos = transform.position;
            tempPos.x = minX;
            transform.position = tempPos;
        }
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

        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("trap");
        }
    }
}
