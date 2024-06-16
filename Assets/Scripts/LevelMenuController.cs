using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private GeneralSounds generalSounds;
    [SerializeField] Button[] buttons;

    public async void BackToMainMenu() 
    {
        await Task.Delay(generalSounds.clickSoundInMs);

        SceneManager.LoadSceneAsync ("MainMenu");
    }


    public async void ResetList()
    {
        await Task.Delay (generalSounds.clickSoundInMs);
        print("deleting all level");

        PlayerPrefs.DeleteAll();

        buttons[0].interactable = true;
        for (int i = 1; i <= 8; i++)
        {
            buttons[i].interactable = false;
            await Task.Yield();
        }
    }
    private void Awake()
    {
        int lockLevel = PlayerPrefs.GetInt("LockLevel",1);

        //* unlock level
        for (int i=0; i < lockLevel; i++)
        {
            print("level unlock: " + i);
            buttons [i].interactable = true;
        }
    }

    //* unlock level
    public async void LevelOpen(int levelId)
    {
        await Task.Delay (generalSounds.clickSoundInMs);
        string levelName = "Level" + levelId;
        SceneManager.LoadScene (levelName);
    }
}
