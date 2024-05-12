using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    [SerializeField] TextMeshProUGUI showAttempts;
    GameObject gameManager;
    private float elapsedTime;
    private int playerAttempts;
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
        }
    }
}
