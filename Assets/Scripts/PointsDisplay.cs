using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    [SerializeField] TextMeshProUGUI showAttempts;
    [SerializeField] TextMeshProUGUI showPoints;
    GameObject gameManager;
    private float elapsedTime;
    private int playerAttempts;
    private int calculatePoints;
    void Start()
    {
        // get time, attempts, coins
        gameManager = GameObject.Find("Game Manager");
        if (gameManager != null)
        {
            elapsedTime = (float) GameManager.elapsedTime.TotalSeconds;
            showTime.text = elapsedTime.ToString("0.00") +"s";

            playerAttempts = GameManager.attempts;
            showAttempts.text = playerAttempts.ToString();

            calculatePoints = (int)(10000 /((elapsedTime)*(playerAttempts/2)));
            if (calculatePoints > 10000)
            calculatePoints = 10000;
            showPoints.text = calculatePoints.ToString();
        }
    }
}
