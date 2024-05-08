using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPageButtons : MonoBehaviour
{
    static bool level2done = false;
    public void NextLevel()
    {
        if (!level2done)
        {
            SceneManager.LoadScene("Level2");
            level2done = true;
        }
            
        else
            SceneManager.LoadScene("Level3");
        Debug.Log("Entering Next Level");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

