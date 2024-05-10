using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPageButtons : MonoBehaviour
{
    GameObject gameManager;
    private string currentLevelName;

    void Awake()
    {
        currentLevelName = GameManager.currentLevelName;
        Debug.Log($"Current level is {currentLevelName}");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
        SceneManager.LoadScene("Level3");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        if (gameManager != null)
            Destroy(gameManager);
    }
}

