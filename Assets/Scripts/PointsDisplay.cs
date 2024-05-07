using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    GameManager gameManager;
    private float elapsedTime;
    void Start()
    {
        // show elapsed time
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        elapsedTime = (float) gameManager.elapsedTime.TotalSeconds;
        showTime.text = elapsedTime.ToString() +"s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
