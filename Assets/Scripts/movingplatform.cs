using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingplatform : MonoBehaviour
{

    [SerializeField]
    private float platformSpeedX, platformSpeedY;

    [SerializeField]
    private float platformMaxPositionX, platformMaxPositionY;

    [SerializeField]
    private float platformMinPositionX, platformMinPositionY;

    private bool movingX;
    private bool movingY;


    // Start is called before the first frame update
    void Start()
    {
        movingX = false;
        movingY = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
