using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPageButtons : MonoBehaviour
{
    GameObject gameManager;
    private string currentLevelName;
    private string nextLevelName;
    private string currentLevelNumChar;
    private int nextLevelInt;

    void Awake()
    {
        Debug.Log("In awake");
        gameManager = GameObject.Find("Game Manager");
        
        if (gameManager != null)
        {
            currentLevelName = GameManager.currentLevelName;
            currentLevelNumChar = currentLevelName[currentLevelName.Length - 1].ToString();
            nextLevelInt = int.Parse(currentLevelNumChar) + 1;
            nextLevelName = "Level" + nextLevelInt.ToString();

            Debug.Log("Game Manager exists");
        }
        
        Debug.Log("next level is " + nextLevelName);

    }
    public void NextLevel()
    {
        Debug.Log("going to " + nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        if (gameManager != null)
            Destroy(gameManager);
    }
}

