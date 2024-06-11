using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EndPageButtons : MonoBehaviour
{
    GameObject gameManager;
    GeneralSounds generalSounds;
    private string currentLevelName;
    private string nextLevelName;
    private string currentLevelNumChar;
    private int nextLevelInt;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        generalSounds = GameObject.Find("Sound Manager").GetComponent<GeneralSounds>();

        //TODO: destroy boss cat in level9

        
        if (gameManager != null)
        {
            currentLevelName = GameManager.currentLevelName;
            currentLevelNumChar = currentLevelName[currentLevelName.Length - 1].ToString();
            nextLevelInt = int.Parse(currentLevelNumChar) + 1;
            nextLevelName = "Level" + nextLevelInt.ToString();
        }
    }
    public async void NextLevel()
    {
        Debug.Log("going to " + nextLevelName);
        await Task.Delay(generalSounds.clickSoundInMs);
        SceneManager.LoadScene(nextLevelName);
    }

    public async void BackToMainMenu()
    {
        await Task.Delay(generalSounds.clickSoundInMs);
        SceneManager.LoadScene("MainMenu");
        if (gameManager != null)
            Destroy(gameManager);
    }
}

