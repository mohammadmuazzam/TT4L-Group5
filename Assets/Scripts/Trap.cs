using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool shouldMoveTrap;
    [SerializeField]
    private float moveAmountX, moveAmountY;
    [SerializeField]
    private float moveSpeed;

    private float finalXPos, finalYPos;
    Vector2 tempPos;
    void Awake()
    {
        finalXPos = transform.position.x + moveAmountX;
        finalYPos = transform.position.y + moveAmountY;
    }

    void Update()
    {
        MoveTrap();
    }   

    void MoveTrap()
    {
        if (shouldMoveTrap)
        {
            tempPos = transform.position;
            if (transform.position.x <= finalXPos) tempPos.x += 0.1f * moveSpeed;
            if (transform.position.y <= finalYPos) tempPos.y += (float) 0.1 * moveSpeed;
            transform.position = tempPos;
        }
        else shouldMoveTrap = false;    
    }
}
