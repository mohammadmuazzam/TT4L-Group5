using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPageButtons : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene("");
        Debug.Log("Entering Next Level");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

