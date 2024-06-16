using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    [SerializeField] TextMeshProUGUI showAttempts;
    [SerializeField] TextMeshProUGUI showPoints;
    [SerializeField] TextMeshProUGUI showCoins;
    GameObject gameManager;
    private float elapsedTime;
    private int playerAttempts;
    private int calculatePoints;

    private int coinsCollected;
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

            coinsCollected = GameManager.coinCount;
            showCoins.text = coinsCollected.ToString();
            
            calculatePoints = (int)(10000 /(elapsedTime*playerAttempts));
            calculatePoints = calculatePoints + 50*coinsCollected;
            print($"elapsed time: {elapsedTime}");
            if (calculatePoints > 10000)
            calculatePoints = 10000;
            showPoints.text = calculatePoints.ToString();
        }
    }
}
