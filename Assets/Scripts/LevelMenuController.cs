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

        PlayerPrefs.DeleteAll();
    }
    private async void Awake()
    {
        int lockLevel = PlayerPrefs.GetInt ("LockLevel",1);
        for (int i=0; i < buttons.Length; i++)
        {
            buttons [i].interactable = false;
        }

        for (int i=0; i < lockLevel; i++)
        {
            buttons [i].interactable = true;
        }
    }

    public async void LevelOpen(int levelId)
    {
        await Task.Delay (generalSounds.clickSoundInMs);
        string levelName = "Level" + levelId;
        SceneManager.LoadScene (levelName);
    }
}
