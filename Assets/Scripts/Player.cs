using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    private Rigidbody2D playerBody;

    private float movementX;
    private bool shouldJump = true;
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

    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f) * speed * Time.deltaTime; 
    }

    void PlayerJump()
    {

    }
}
