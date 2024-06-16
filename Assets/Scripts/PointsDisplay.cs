using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    [SerializeField] TextMeshProUGUI showAttempts;
    [SerializeField] TextMeshProUGUI showPoints;
    [SerializeField] TextMeshProUGUI showCoins;
    [SerializeField] Button nextLevel;
    GameObject gameManager;
    private float elapsedTime;
    private int playerAttempts;
    private int calculatePoints;

    private int coinsCollected;
    void Awake()
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
            
            calculatePoints = (int)(10000 /(elapsedTime*playerAttempts));
            

            //* if final level
            if (GameManager.currentLevelName != "Level9")
            {
                calculatePoints = calculatePoints + 50*coinsCollected;
                showCoins.text = coinsCollected.ToString();
                nextLevel.enabled = true;
            }
            else 
            {
                calculatePoints = calculatePoints*100;
                showCoins.text = "-";
                nextLevel.enabled = false;
            }

            if (calculatePoints > 10000)
                calculatePoints = 10000;

            showPoints.text = calculatePoints.ToString();

            if (calculatePoints < 0)
                showPoints.text = "That's crazy";
        }
    }
}
