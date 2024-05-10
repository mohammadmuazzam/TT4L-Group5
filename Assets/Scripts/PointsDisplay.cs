using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI showTime;
    GameObject gameManager;
    private float elapsedTime;
    void Start()
    {
        // show elapsed time
        gameManager = GameObject.Find("Game Manager");
        if (gameManager != null)
        {
            elapsedTime = (float) GameManager.elapsedTime.TotalSeconds;
            showTime.text = elapsedTime.ToString("0.00") +"s";
        }
    }
}
