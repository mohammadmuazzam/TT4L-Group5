using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    private Rigidbody2D playerBody;

    private float movementX;
    private bool shouldJump = false;
    private bool isOnGround = false;
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("Jumping");
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            shouldJump = false;
        }
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f) * speed * Time.deltaTime;
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            isOnGround = false;
            shouldJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
    }
}
